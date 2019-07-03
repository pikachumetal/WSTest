//----------------------------------------------------------------
// Copyright (c) Microsoft Corporation.  All rights reserved.
//----------------------------------------------------------------

using System;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.ServiceModel.Channels;

namespace WsAncertCommunication.Bindings.CustomTextMessage
{
    class MessageVersionConverter : TypeConverter
    {
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            if (typeof(string) == sourceType) return true;
            return base.CanConvertFrom(context, sourceType);
        }

        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            if (typeof(InstanceDescriptor) == destinationType) return true;
            return base.CanConvertTo(context, destinationType);
        }

        public override object ConvertFrom(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value)
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

            throw new ArgumentOutOfRangeException("messageVersion");
        }

        public override object ConvertTo(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value, Type destinationType)
        {
            var isDestinationTypeAString = typeof(string) == destinationType;
            var isValueAMessageVersion = value is MessageVersion;

            if (!isDestinationTypeAString || !isValueAMessageVersion) return base.ConvertTo(context, culture, value, destinationType);

            var messageVersion = (MessageVersion)value;

            if (messageVersion == MessageVersion.Default)
            {
                return ConfigurationStrings.Default;
            }
            if (messageVersion == MessageVersion.Soap11WSAddressing10)
            {
                return ConfigurationStrings.Soap11WSAddressing10;
            }
            if (messageVersion == MessageVersion.Soap12WSAddressing10)
            {
                return ConfigurationStrings.Soap12WSAddressing10;
            }
            if (messageVersion == MessageVersion.Soap11WSAddressingAugust2004)
            {
                return ConfigurationStrings.Soap11WSAddressingAugust2004;
            }
            if (messageVersion == MessageVersion.Soap12WSAddressingAugust2004)
            {
                return ConfigurationStrings.Soap12WSAddressingAugust2004;
            }
            if (messageVersion == MessageVersion.Soap11)
            {
                return ConfigurationStrings.Soap11;
            }
            if (messageVersion == MessageVersion.Soap12)
            {
                return ConfigurationStrings.Soap12;
            }
            if (messageVersion == MessageVersion.None)
            {
                return ConfigurationStrings.None;
            }

            throw new ArgumentOutOfRangeException("messageVersion");
        }
    }
}
