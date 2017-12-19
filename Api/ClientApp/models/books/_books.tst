$Classes(BCRBiddingWeb.Models.Cms.*)[
import { $Properties[$ImportCms] } from '../cms'
import { $Properties[$ImportEnums] } from '../enums'

export interface I$Name {
	$Properties[$Declaration
	]}
]
${
    string[] namespaces() {
        return new string[] { 
            "BCRBiddingWeb.Models.Cms",
            "BE.Types"
        };
    }

    string Declaration(Property property) {
        var format = property.Type.IsEnumerable ? "I{0}[]" : "I{0}";
        var typeName = GetType(property, namespaces()[0], format) ?? property.Type.Name;
        return decapitalize(property.Name) + ": " + typeName;
    }
    
	string ImportEnums(Property property) {
		return GetType(property, namespaces()[1]);
	}

	string ImportCms(Property property) {
		return GetType(property, namespaces()[0], "I{0}");
	}

    string GetType(Property property) {
        return namespaces()
            .Select(ns => GetType(property, ns))
            .FirstOrDefault(t => t != null);
    }

	string GetType(Property property, string _namespace, string format = null) {
        var result = "";
		if(property.Type.FullName.Contains(_namespace)) {
			if(property.Type.IsEnumerable) 
			{
				result = property.Type.TypeArguments.FirstOrDefault()?.Name;
			}
			else 
			{
				result = property.Type.Name;
			}

            

            return (format != null ? String.Format(format, result) : result) + ",";
		}
			
		return null;
	}

    string decapitalize(string value) {
        return Char.ToLowerInvariant(value[0]) + value.Substring(1);
    }
}