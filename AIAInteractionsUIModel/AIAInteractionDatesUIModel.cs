namespace AIAInteractionsUIModel
{

    public partial class AIAInteractionDatesUIModel
    {

        private void AIAInteractionDatesUIModel_Loaded(object sender, Blackbaud.AppFx.UIModeling.Core.LoadedEventArgs e)
        {
            RECORDTYPEID.Value = Blackbaud.AppFx.Constituent.UIModel.GlobalChangeHelper.GetRecordTypeID("INTERACTION", GetRequestContext());
        }

#region "Event handlers"

        partial void OnCreated()
        {
            this.Loaded += AIAInteractionDatesUIModel_Loaded;
        }

#endregion

    }

}