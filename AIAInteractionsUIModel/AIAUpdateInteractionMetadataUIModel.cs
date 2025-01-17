using System;
using Blackbaud.AppFx.UIModeling.Core;

namespace AIAInteractionsUIModel
{

	public partial class AIAUpdateInteractionMetadataUIModel
	{

        private void SetActual()
        {
            ACTUALDATE.Enabled = !ACTUALDATERELATIVE.Value;
            ACTUALDATEDAYS.Enabled = ACTUALDATERELATIVE.Value;
            if (ACTUALDATERELATIVE.Value == false)
            {
                ACTUALDATEDAYS.Value = 0;
            }
            else
            {
                ACTUALDATE.Value = DateTime.MinValue;
            }
        }

        private void SetExpected()
        {
            EXPECTEDDATE.Enabled = !EXPECTEDDATERELATIVE.Value;
            EXPECTEDDATEDAYS.Enabled = EXPECTEDDATERELATIVE.Value;
            if (EXPECTEDDATERELATIVE.Value == false)
            {
                EXPECTEDDATEDAYS.Value = 0;
            }
            else
            {
                EXPECTEDDATE.Value = DateTime.MinValue;
            }
        }

        private void _actualdaterelative_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            SetActual();
        }

        private void _expecteddaterelative_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            SetExpected();
        }

        private void UpdateSubCategoryEnabledRequired()
        {
            bool flag = _interactioncategoryid.HasValue() && _interactioncategoryid.Value != Guid.Empty;
            _interactionsubcategoryid.Enabled = flag;
            _interactionsubcategoryid.Required = flag;
        }

        private void _interactioncategoryid_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            if (!this.Loading)
            {
                _interactionsubcategoryid.Value = Guid.Empty;
            }
            _interactionsubcategoryid.ResetDataSource();
            UpdateSubCategoryEnabledRequired();
        }

		private void AIAUpdateInteractionMetadataUIModel_Loaded(object sender, LoadedEventArgs e)
		{
            RECORDTYPEID.Value = Blackbaud.AppFx.Constituent.UIModel.GlobalChangeHelper.GetRecordTypeID("INTERACTION", GetRequestContext());
            UpdateSubCategoryEnabledRequired();
            SetActual();
            SetExpected();
		}

#region "Event handlers"

		partial void OnCreated()
		{
			this.Loaded += AIAUpdateInteractionMetadataUIModel_Loaded;
            this._interactioncategoryid.ValueChanged += _interactioncategoryid_ValueChanged;
            this._actualdaterelative.ValueChanged += _actualdaterelative_ValueChanged;
            this._expecteddaterelative.ValueChanged += _expecteddaterelative_ValueChanged;
		}

#endregion

	}

}