using JetBrains.ActionManagement;
using JetBrains.Application.DataContext;
using JetBrains.ReSharper.Features.Common.Src.Options;
using JetBrains.UI.Icons;
using JetBrains.Util;

namespace CitizenMatt.ReSharper.Plugins.DiffGold
{
    [ActionHandler("DiffGold.UpdateGold")]
    public class UpdateGoldAction : GoldActionBase
    {
        protected override bool Update(IDataContext context, ActionPresentation presentation, FileSystemPath goldPath, FileSystemPath tempPath)
        {
            return !goldPath.IsEmpty && !tempPath.IsEmpty;
        }

        protected override void Execute(IDataContext context, FileSystemPath goldPath, FileSystemPath tempPath)
        {
            tempPath.CopyFile(goldPath, true);
        }

        protected override IconId IconId
        {
            get { return CommonFeaturesOptionsThemedIcons.Inc.Id; }
        }
    }
}