﻿using OfficeDevPnP.Core.Framework.Provisioning.Model;
using OfficeDevPnP.Core.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Schema;
using System.Xml.Serialization;
using OfficeDevPnP.Core.Framework.Provisioning.Providers.Xml.V201605;
using ContentType = OfficeDevPnP.Core.Framework.Provisioning.Model.ContentType;
using OfficeDevPnP.Core.Extensions;
using Microsoft.SharePoint.Client;
using Newtonsoft.Json.Serialization;
using OfficeDevPnP.Core.Framework.Provisioning.Providers.Xml.Serializers;
using FileLevel = OfficeDevPnP.Core.Framework.Provisioning.Model.FileLevel;

namespace OfficeDevPnP.Core.Framework.Provisioning.Providers.Xml
{
    /// <summary>
    /// Implements the logic to serialize a schema of version 201605
    /// </summary>
    internal class XMLPnPSchemaV201605Serializer : XmlPnPSchemaBaseSerializer<V201605.ProvisioningTemplate>
    {
        public XMLPnPSchemaV201605Serializer():
            base(typeof(XMLConstants)
                .Assembly
                .GetManifestResourceStream("OfficeDevPnP.Core.Framework.Provisioning.Providers.Xml.ProvisioningSchema-2016-05.xsd"))
        {
        }

        public override string NamespaceUri
        {
            get { return (XMLConstants.PROVISIONING_SCHEMA_NAMESPACE_2016_05); }
        }

        public override string NamespacePrefix
        {
            get { return (XMLConstants.PROVISIONING_SCHEMA_PREFIX); }
        }

        protected override void DeserializeTemplate(object persistenceTemplate, Model.ProvisioningTemplate template)
        {
            var tbps = new TemplateBasePropertiesSerializer();
            tbps.Deserialize(persistenceTemplate, template);

            var pbs = new PropertyBagPropertiesSerializer();
            pbs.Deserialize(persistenceTemplate, template);

            var lis = new ListInstancesSerializer();
            lis.Deserialize(persistenceTemplate, template);
        }

        protected override void SerializeTemplate(Model.ProvisioningTemplate template, object persistenceTemplate)
        {
            var tbps = new TemplateBasePropertiesSerializer();
            tbps.Serialize(template, persistenceTemplate);

            var pbs = new PropertyBagPropertiesSerializer();
            pbs.Serialize(template, persistenceTemplate);

            var lis = new ListInstancesSerializer();
            lis.Serialize(template, persistenceTemplate);
        }
    }
}
