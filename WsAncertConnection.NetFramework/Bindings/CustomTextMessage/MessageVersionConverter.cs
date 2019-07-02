using System;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Globalization;
using System.ServiceModel.Channels;

namespace WsAncertConnection.NetFramework.Bindings.CustomTextMessage
{
    internal class MessageVersionConverter : TypeConverter
    {
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            return typeof(string) == sourceType || base.CanConvertFrom(context, sourceType);
        }

        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            return typeof(InstanceDescriptor) == destinationType || base.CanConvertTo(context, destinationType);
        }

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            var isValueAString = value is string;
            if (!isValueAString) return base.ConvertFrom(context, culture, value);

            var messageVersion = (string)value;
            switch (messageVersion)
            {
                case ConfigurationStrings.Soap11WSAddressing10:
                    return MessageVersion.Soap11WSAddressing10;
                case ConfigurationStrings.Soap12WSAddressing10:
                    return MessageVersion.Soap12WSAddressing10;
                case ConfigurationStrings.Soap11WSAddressingAugust2004:
                    return MessageVersion.Soap11WSAddressingAugust2004;
                case ConfigurationStrings.Soap12WSAddressingAugust2004:
                    return MessageVersion.Soap12WSAddressingAugust2004;
                case ConfigurationStrings.Soap11:
                    return MessageVersion.Soap11;
                case ConfigurationStrings.Soap12:
                    return MessageVersion.Soap12;
                case ConfigurationStrings.None:
                    return MessageVersion.None;
                case ConfigurationStrings.Default:
                    return MessageVersion.Default;
            }

            throw new ArgumentOutOfRangeException(nameof(MessageVersion));
        }

        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            var isDestinationTypeAString = typeof(string) == destinationType;
            var isValueAMessageVersion = value is MessageVersion;

            if (!isDestinationTypeAString || !isValueAMessageVersion) return base.ConvertTo(context, culture, value, destinationType);

            var messageVersion = (MessageVersion)value;

            if (Equals(messageVersion, MessageVersion.Default))
            {
                return ConfigurationStrings.Default;
            }
            if (Equals(messageVersion, MessageVersion.Soap11WSAddressing10))
            {
                return ConfigurationStrings.Soap11WSAddressing10;
            }
            if (Equals(messageVersion, MessageVersion.Soap12WSAddressing10))
            {
                return ConfigurationStrings.Soap12WSAddressing10;
            }
            if (Equals(messageVersion, MessageVersion.Soap11WSAddressingAugust2004))
            {
                return ConfigurationStrings.Soap11WSAddressingAugust2004;
            }
            if (Equals(messageVersion, MessageVersion.Soap12WSAddressingAugust2004))
            {
                return ConfigurationStrings.Soap12WSAddressingAugust2004;
            }
            if (Equals(messageVersion, MessageVersion.Soap11))
            {
                return ConfigurationStrings.Soap11;
            }
            if (Equals(messageVersion, MessageVersion.Soap12))
            {
                return ConfigurationStrings.Soap12;
            }
            if (Equals(messageVersion, MessageVersion.None))
            {
                return ConfigurationStrings.None;
            }

            throw new ArgumentOutOfRangeException(nameof(context));
        }
    }
}
