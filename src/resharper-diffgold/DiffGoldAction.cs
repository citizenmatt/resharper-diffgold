using JetBrains.ActionManagement;
using JetBrains.Application.DataContext;
using JetBrains.UI.Icons;
using JetBrains.UI.Resources;
using JetBrains.Util;
using Microsoft.VisualStudio.Shell.Interop;

namespace CitizenMatt.ReSharper.DiffGold
{
    [ActionHandler("DiffGold.DiffGold")]
    public class DiffGoldAction : GoldActionBase
    {
        protected override bool Update(IDataContext context, ActionPresentation presentation,
            FileSystemPath goldPath, FileSystemPath tempPath)
        {
            return !goldPath.IsEmpty && !tempPath.IsEmpty;
        }

        protected override void Execute(IDataContext context, FileSystemPath goldPath, FileSystemPath tempPath)
        {
            if (goldPath.IsEmpty || tempPath.IsEmpty)
                return;
            
            var differenceService = context.TryGetComponent<IVsDifferenceService>();
            if (differenceService == null)
                return;

            var caption = string.Format("{0} vs {1}", tempPath.Name, goldPath.Name);
            const __VSDIFFSERVICEOPTIONS options = 0;
            differenceService.OpenComparisonWindow2(tempPath.FullPath, goldPath.FullPath, caption, caption,
                tempPath.Name + " (readonly)", goldPath.Name,
                inlineLabel: string.Empty, roles: string.Empty, grfDiffOptions: (uint) options);
        }

        protected override IconId IconId
        {
            get { return CommonThemedIcons.Duplicate.Id; }
        }
    }
}
