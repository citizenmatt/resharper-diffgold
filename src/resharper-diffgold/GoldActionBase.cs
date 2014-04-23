using System;
using System.Linq;
using System.Text.RegularExpressions;
using JetBrains.ActionManagement;
using JetBrains.Application.DataContext;
using JetBrains.IDE.TreeBrowser;
using JetBrains.ReSharper.TaskRunnerFramework;
using JetBrains.ReSharper.UnitTestExplorer.Session;
using JetBrains.ReSharper.UnitTestFramework;
using JetBrains.UI.Icons;
using JetBrains.Util;

namespace CitizenMatt.ReSharper.Plugins.DiffGold
{
    public abstract class GoldActionBase : IActionHandler
    {
        private static readonly Regex GoldRegex = new Regex(@"^Data.GoldFile\s+=\s+file:///(?<path>.+?)\#.+?$",
            RegexOptions.Compiled | RegexOptions.Multiline | RegexOptions.Singleline);
        private static readonly Regex TempRegex = new Regex(@"^Data.TempFile\s+=\s+file:///(?<path>.+?)\#.+?$",
            RegexOptions.Compiled | RegexOptions.Multiline | RegexOptions.Singleline);

        public bool Update(IDataContext context, ActionPresentation presentation, DelegateUpdate nextUpdate)
        {
            var retval = false;
            WithGoldFiles(context, (goldPath, tempPath) => retval = Update(context, presentation, goldPath, tempPath));

            // This is both cheeky and lazy. I should set icons in the actions.xml, but that requires
            // the icons being defined in this assembly, and I can't be bothered
            if (presentation.GetImage() == null)
                presentation.SetImage(IconId);

            return retval;
        }

        public void Execute(IDataContext context, DelegateExecute nextExecute)
        {
            WithGoldFiles(context, (goldPath, tempPath) => Execute(context, goldPath, tempPath));
        }

        private void WithGoldFiles(IDataContext context, Action<FileSystemPath, FileSystemPath> action)
        {
            // TODO: Can I stuff this into the context during update?
            var session = context.GetData(TreeModelBrowser.TREE_MODEL_DESCRIPTOR) as UnitTestSessionView;
            if (session == null)
                return;

            var resultManager = context.GetComponent<IUnitTestResultManager>();

            var element = session.ActiveElement;
            if (element != null)
            {
                var result = resultManager.GetResult(element);
                var resultData = resultManager.GetResultData(element);

                if (result.RunStatus == UnitTestRunStatus.Running)
                    return;

                var exceptions = resultData.Exceptions ?? EmptyArray<TaskException>.Instance;

                var goldPath = exceptions.Select(e => GetPath(GoldRegex, e)).FirstOrDefault(FileSystemPath.Empty);
                var tempPath = exceptions.Select(e => GetPath(TempRegex, e)).FirstOrDefault(FileSystemPath.Empty);

                action(goldPath, tempPath);
            }
        }

        private static FileSystemPath GetPath(Regex regex, TaskException taskException)
        {
            var match = regex.Match(taskException.Message);
            return !match.Success ? FileSystemPath.Empty : FileSystemPath.TryParse(match.Groups["path"].Value);
        }

        protected abstract IconId IconId { get; }
        protected abstract bool Update(IDataContext context, ActionPresentation presentation,
            FileSystemPath goldPath, FileSystemPath tempPath);
        protected abstract void Execute(IDataContext context, FileSystemPath goldPath, FileSystemPath tempPath);
    }
}