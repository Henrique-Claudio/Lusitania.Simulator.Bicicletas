using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI.WebControls;
using System.Web.UI;

namespace Lusitania.Simuladores.Web.UI.WebControls
{
    public class AjaxButton : WebControl, IButtonControl, INamingContainer
    {
        public AjaxButton()
        {
            mUpdatePanel = new UpdatePanel();
            mUpdatePanel.ID = "mUpdatePanel";
            mUpdatePanel.UpdateMode = UpdatePanelUpdateMode.Conditional;

            mButton = new Button();
            mButton.ID = "mButton";
            mButton.Click += new EventHandler(mButton_Click);
            mButton.Command += new CommandEventHandler(mButton_Command);
        }

        #region Fields

        private UpdatePanel mUpdatePanel;
        private Button mButton;

        #endregion

        #region Methods

        protected override void CreateChildControls()
        {
            // add the update panel
            Controls.Add(mUpdatePanel);

            // add the button to the update panel
            mUpdatePanel.ContentTemplateContainer.Controls.Add(mButton);

            // done
            base.CreateChildControls();
        }

        private void mButton_Click(object sender, EventArgs e)
        {
            OnClick();
        }

        private void mButton_Command(object sender, CommandEventArgs e)
        {
            OnCommand(e);
        }

        #endregion

        #region IButtonControl Members

        public bool CausesValidation
        {
            get
            {
                return mButton.CausesValidation;
            }
            set
            {
                if (mButton.CausesValidation != value)
                {
                    mButton.CausesValidation = value;
                    if (!DesignMode)
                    {
                        mUpdatePanel.Update();
                    }
                }
            }
        }

        public event EventHandler Click;
        protected virtual void OnClick()
        {
            if (Click != null)
            {
                Click(this, EventArgs.Empty);
            }
        }

        public event CommandEventHandler Command;
        protected virtual void OnCommand(CommandEventArgs e)
        {
            if (Command != null)
            {
                Command(this, e);
            }
        }

        public string CommandArgument
        {
            get
            {
                return mButton.CommandArgument;
            }
            set
            {
                if (mButton.CommandArgument != value)
                {
                    mButton.CommandArgument = value;
                    if (!DesignMode)
                    {
                        mUpdatePanel.Update();
                    }
                }
            }
        }

        public string CommandName
        {
            get
            {
                return mButton.CommandName;
            }
            set
            {
                if (mButton.CommandName != value)
                {
                    mButton.CommandName = value;
                    if (!DesignMode)
                    {
                        mUpdatePanel.Update();
                    }
                }
            }
        }

        public string PostBackUrl
        {
            get
            {
                return mButton.PostBackUrl;
            }
            set
            {
                if (mButton.PostBackUrl != value)
                {
                    mButton.PostBackUrl = value;
                    if (!DesignMode)
                    {
                        mUpdatePanel.Update();
                    }
                }
            }
        }

        public string Text
        {
            get
            {
                return mButton.Text;
            }
            set
            {
                if (mButton.Text != value)
                {
                    mButton.Text = value;
                    if (!DesignMode)
                    {
                        mUpdatePanel.Update();
                    }
                }
            }
        }

        public string ValidationGroup
        {
            get
            {
                return mButton.ValidationGroup;
            }
            set
            {
                if (mButton.ValidationGroup != value)
                {
                    mButton.ValidationGroup = value;
                    if (!DesignMode)
                    {
                        mUpdatePanel.Update();
                    }
                }
            }
        }

        #endregion
    }
}
