using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Configuration;

namespace ConsoleApp1
{
    public class AsymetricSecurityBE : BindingElement
    {
        private AsymmetricSecurityBindingElement m_asymSecBE;
        public AsymetricSecurityBE()
        {
            var securityVersion = MessageSecurityVersion.WSSecurity10WSTrustFebruary2005WSSecureConversationFebruary2005WSSecurityPolicy11BasicSecurityProfile10;
            var securityBE = SecurityBindingElement.CreateMutualCertificateBindingElement(securityVersion, true);
            securityBE.IncludeTimestamp = true;
            securityBE.SetKeyDerivation(false);
            securityBE.SecurityHeaderLayout = SecurityHeaderLayout.Lax;
            securityBE.EnableUnsecuredResponse = true;
            m_asymSecBE = securityBE as AsymmetricSecurityBindingElement;
        }

        public AsymetricSecurityBE(AsymetricSecurityBE other)
        {
            m_asymSecBE = other.m_asymSecBE;
            // 6 Steps to add a security header to every request from a.net client
        }

        public override IChannelListener<TChannel> BuildChannelListener<TChannel>(BindingContext context)
        {
            return m_asymSecBE.BuildChannelListener<TChannel>(context);
        }

        public override IChannelFactory<TChannel> BuildChannelFactory<TChannel>(BindingContext context)
        {
            return m_asymSecBE.BuildChannelFactory<TChannel>(context);
        }

        public override BindingElement Clone()
        {
            AsymetricSecurityBE ret = new AsymetricSecurityBE(this);
            return ret;
        }

        public override T GetProperty<T>(BindingContext context)
        {
            return m_asymSecBE.GetProperty<T>(context);
        }
    }

    class AsymetricSecurityBEExtentionElement : BindingElementExtensionElement
    {
        public override Type BindingElementType
        {
            get { return typeof(AsymetricSecurityBE); }
        }

        protected override BindingElement CreateBindingElement()
        {
            return new AsymetricSecurityBE();
        }
    }
}