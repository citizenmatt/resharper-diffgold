using JetBrains.ActionManagement;
using JetBrains.Application.DataContext;
using JetBrains.IDE;
using JetBrains.UI.Icons;
using JetBrains.UI.Resources;
using JetBrains.Util;

namespace CitizenMatt.ReSharper.DiffGold
{
    [ActionHandler("DiffGold.EditGold")]
    public class EditGoldAction : GoldActionBase
    {
        protected override bool Update(IDataContext context, ActionPresentation presentation,
            FileSystemPath goldPath, FileSystemPath tempPath)
        {
            return !goldPath.IsEmpty;
        }

        protected override void Execute(IDataContext context, FileSystemPath goldPath, FileSystemPath tempPath)
        {
            var editorManager = context.GetComponent<EditorManager>();
            editorManager.OpenFile(goldPath, true, TabOptions.NormalTab);
        }

        protected override IconId IconId
        {
            get { return CommonThemedIcons.Document.Id; }
        }
    }
}