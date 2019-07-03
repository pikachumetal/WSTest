//----------------------------------------------------------------
// Copyright (c) Microsoft Corporation.  All rights reserved.
//----------------------------------------------------------------

using ConsoleApp1.Bindings.CustomTextMessage;
using System;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.Xml;

namespace Microsoft.Samples.CustomTextMessageEncoder
{
    public class CustomTextMessageBindingElement : MessageEncodingBindingElement, IWsdlExportExtension
    {
        private MessageVersion _msgVersion;
        private string _mediaType;
        private string _encoding;
        private readonly XmlDictionaryReaderQuotas _readerQuotas;

        CustomTextMessageBindingElement(CustomTextMessageBindingElement binding)
            : this(binding.Encoding, binding.MediaType, binding.MessageVersion)
        {
            _readerQuotas = new XmlDictionaryReaderQuotas();
            binding.ReaderQuotas.CopyTo(_readerQuotas);
        }

        public CustomTextMessageBindingElement(string encoding, string mediaType, MessageVersion msgVersion)
        {
            _msgVersion = msgVersion ?? throw new ArgumentNullException("msgVersion");
            _mediaType = mediaType ?? throw new ArgumentNullException("mediaType");
            _encoding = encoding ?? throw new ArgumentNullException("encoding");
            _readerQuotas = new XmlDictionaryReaderQuotas();
        }

        public CustomTextMessageBindingElement(string encoding, string mediaType)
            : this(encoding, mediaType, MessageVersion.Soap11WSAddressing10) { }

        public CustomTextMessageBindingElement(string encoding) : this(encoding, "text/xml") { }

        public CustomTextMessageBindingElement() : this("UTF-8") { }

        public override MessageVersion MessageVersion
        {
            get => _msgVersion;

            set
            {
                _msgVersion = value ?? throw new ArgumentNullException("value");
            }
        }


        public string MediaType
        {
            get => _mediaType;

            set
            {
                _mediaType = value ?? throw new ArgumentNullException("value");
            }
        }

        public string Encoding
        {
            get
            {
                return _encoding;
            }

            set
            {
                _encoding = value ?? throw new ArgumentNullException("value");
            }
        }

        // This encoder does not enforces any quotas for the unsecure messages. The 
        // quotas are enforced for the secure portions of messages when this encoder
        // is used in a binding that is configured with security. 
        public XmlDictionaryReaderQuotas ReaderQuotas => _readerQuotas;
        
        public override BindingElement Clone()
        {
            return new CustomTextMessageBindingElement(this);
        }

        public override IChannelFactory<TChannel> BuildChannelFactory<TChannel>(BindingContext context)
        {
            AsserContextIsNotNull(context);

            context.BindingParameters.Add(this);
            return context.BuildInnerChannelFactory<TChannel>();
        }

        private static void AsserContextIsNotNull(BindingContext context)
        {
            if (context == null) throw new ArgumentNullException("context");
        }

        public override bool CanBuildChannelFactory<TChannel>(BindingContext context)
        {
            AsserContextIsNotNull(context);

            return context.CanBuildInnerChannelFactory<TChannel>();
        }

        public override IChannelListener<TChannel> BuildChannelListener<TChannel>(BindingContext context)
        {
            AsserContextIsNotNull(context);

            context.BindingParameters.Add(this);
            return context.BuildInnerChannelListener<TChannel>();
        }

        public override bool CanBuildChannelListener<TChannel>(BindingContext context)
        {
            AsserContextIsNotNull(context);

            context.BindingParameters.Add(this);
            return context.CanBuildInnerChannelListener<TChannel>();
        }

        public override T GetProperty<T>(BindingContext context)
        {
            if (typeof(T) == typeof(XmlDictionaryReaderQuotas))
            {
                return (T)(object)_readerQuotas;
            }
            else
            {
                return base.GetProperty<T>(context);
            }
        }

        #region IMessageEncodingBindingElement Members

        public override MessageEncoderFactory CreateMessageEncoderFactory()
        {
            return new CustomTextMessageEncoderFactory(MediaType, Encoding, MessageVersion);
        }

        #endregion

        #region IWsdlExportExtension Members

        void IWsdlExportExtension.ExportContract(WsdlExporter exporter, WsdlContractConversionContext context) { }

        void IWsdlExportExtension.ExportEndpoint(WsdlExporter exporter, WsdlEndpointConversionContext context)
        {
            // The MessageEncodingBindingElement is responsible for ensuring that the WSDL has the correct
            // SOAP version. We can delegate to the WCF implementation of TextMessageEncodingBindingElement for this.
            var mebe = new TextMessageEncodingBindingElement { MessageVersion = _msgVersion };
            ((IWsdlExportExtension)mebe).ExportEndpoint(exporter, context);
        }

        #endregion
    }
}
