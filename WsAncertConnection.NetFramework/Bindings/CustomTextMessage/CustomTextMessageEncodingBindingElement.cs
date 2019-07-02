using System;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.Xml;

namespace WsAncertConnection.NetFramework.Bindings.CustomTextMessage
{
    public class CustomTextMessageBindingElement : MessageEncodingBindingElement, IWsdlExportExtension
    {
        private MessageVersion _msgVersion;
        private string _mediaType;
        private string _encoding;

        public CustomTextMessageBindingElement(string encoding, string mediaType, MessageVersion msgVersion)
        {
            _msgVersion = msgVersion ?? throw new ArgumentNullException(nameof(msgVersion));
            _mediaType = mediaType ?? throw new ArgumentNullException(nameof(mediaType));
            _encoding = encoding ?? throw new ArgumentNullException(nameof(encoding));
            ReaderQuotas = new XmlDictionaryReaderQuotas();
        }

        public CustomTextMessageBindingElement(string encoding, string mediaType)
            : this(encoding, mediaType, MessageVersion.Soap11WSAddressing10) { }

        public CustomTextMessageBindingElement(string encoding)
            : this(encoding, "text/xml") { }

        public CustomTextMessageBindingElement()
            : this("UTF-8") { }

        private CustomTextMessageBindingElement(CustomTextMessageBindingElement binding)
            : this(binding.Encoding, binding.MediaType, binding.MessageVersion)
        {
            ReaderQuotas = new XmlDictionaryReaderQuotas();
            binding.ReaderQuotas.CopyTo(ReaderQuotas);
        }

        public override MessageVersion MessageVersion
        {
            get => _msgVersion;
            set => _msgVersion = value ?? throw new ArgumentNullException(nameof(value));
        }


        public string MediaType
        {
            get => _mediaType;
            set => _mediaType = value ?? throw new ArgumentNullException(nameof(value));
        }

        public string Encoding
        {
            get => _encoding;
            set => _encoding = value ?? throw new ArgumentNullException(nameof(value));
        }

        // This encoder does not enforces any quotas for the unsure messages. The 
        // quotas are enforced for the secure portions of messages when this encoder
        // is used in a binding that is configured with security. 
        public XmlDictionaryReaderQuotas ReaderQuotas { get; }

        public override BindingElement Clone() => new CustomTextMessageBindingElement(this);

        public override IChannelFactory<TChannel> BuildChannelFactory<TChannel>(BindingContext context)
        {
            AssertContextIsNotNull(context);

            context.BindingParameters.Add(this);
            return context.BuildInnerChannelFactory<TChannel>();
        }

        private static void AssertContextIsNotNull(BindingContext context)
        {
            if (context == null) throw new ArgumentNullException(nameof(context));
        }

        public override bool CanBuildChannelFactory<TChannel>(BindingContext context)
        {
            AssertContextIsNotNull(context);

            return context.CanBuildInnerChannelFactory<TChannel>();
        }

        public override IChannelListener<TChannel> BuildChannelListener<TChannel>(BindingContext context)
        {
            AssertContextIsNotNull(context);

            context.BindingParameters.Add(this);
            return context.BuildInnerChannelListener<TChannel>();
        }

        public override bool CanBuildChannelListener<TChannel>(BindingContext context)
        {
            AssertContextIsNotNull(context);

            context.BindingParameters.Add(this);
            return context.CanBuildInnerChannelListener<TChannel>();
        }

        public override T GetProperty<T>(BindingContext context)
        {
            if (typeof(T) == typeof(XmlDictionaryReaderQuotas))
            {
                return (T)(object)ReaderQuotas;
            }

            return base.GetProperty<T>(context);
        }

        #region IMessageEncodingBindingElement Members

        public override MessageEncoderFactory CreateMessageEncoderFactory()
            => new CustomTextMessageEncoderFactory(MediaType, Encoding, MessageVersion);

        #endregion

        #region IWsdlExportExtension Members

        void IWsdlExportExtension.ExportContract(WsdlExporter exporter, WsdlContractConversionContext context) { }

        void IWsdlExportExtension.ExportEndpoint(WsdlExporter exporter, WsdlEndpointConversionContext context)
        {
            // The MessageEncodingBindingElement is responsible for ensuring that the WSDL has the correct
            // SOAP version. We can delegate to the WCF implementation of TextMessageEncodingBindingElement for this.
            var bindingElement = new TextMessageEncodingBindingElement { MessageVersion = _msgVersion };
            ((IWsdlExportExtension)bindingElement).ExportEndpoint(exporter, context);
        }

        #endregion
    }
}
