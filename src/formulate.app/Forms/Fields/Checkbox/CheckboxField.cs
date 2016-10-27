﻿namespace formulate.app.Forms.Fields.Checkbox
{
    using System;
    using System.Collections.Generic;
    public class CheckboxField : IFormFieldType
    {
        public string Directive => "formulate-checkbox-field";
        public string TypeLabel => "Checkbox";
        public string Icon => "icon-checkbox-dotted";
        public Guid TypeId => new Guid("99C1A656C7A644ACA787C18593327310");
        public object DeserializeConfiguration(string configuration)
        {
            return null;
        }
        public string FormatValue(IEnumerable<string> values, FieldPresentationFormats format)
        {
            return string.Join(", ", values);
        }
    }
}