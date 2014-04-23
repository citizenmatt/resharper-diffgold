using JetBrains.ActionManagement;
using JetBrains.Application.DataContext;
using JetBrains.IDE;
using JetBrains.ReSharper.Feature.Services.Resources;
using JetBrains.UI.Icons;
using JetBrains.Util;

namespace CitizenMatt.ReSharper.DiffGold
{
    [ActionHandler("DiffGold.EditTemp")]
    public class EditTempAction : GoldActionBase
    {
        protected override bool Update(IDataContext context, ActionPresentation presentation,
            FileSystemPath goldPath, FileSystemPath tempPath)
        {
            return !tempPath.IsEmpty;
        }

        protected override void Execute(IDataContext context, FileSystemPath goldPath, FileSystemPath tempPath)
        {
            var editorManager = context.GetComponent<EditorManager>();
            editorManager.OpenFile(tempPath, true, TabOptions.NormalTab);
        }

        protected override IconId IconId
        {
            get { return ServicesThemedIcons.FileTemplate.Id; }
        }
    }
}