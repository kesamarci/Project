using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using static Project.Models.Attributumos.Attri;
using Project.Models.Attributumos;

namespace Project.Models.Attributumos
{
    
        public class DataFetcher
        {
            public void FetchDataFromProgram()
            {
                Assembly assem = Assembly.GetExecutingAssembly();

                Type[] types = assem.GetTypes();

                foreach (var item in types)
                {
                    Console.WriteLine(item);
                }

                IEnumerable<Type> objectTypes = types.Where(x => x.GetCustomAttribute<ToExportAttribute>() != null);
                IEnumerable<Type> objectTypesHide = types.Where(x => x.GetCustomAttribute<HideFromExportAttribute>() != null);

            DateTime fileCreationDate = DateTime.Now;

                XDocument xdoc = new XDocument();
                xdoc.Add(new XElement("entities",
                    new XAttribute("exportDate", fileCreationDate.ToString("yyyy.MM.dd. HH:mm:ss")) // Dátum formázása és hozzáadása attribútumként
                ));
           

                foreach (var item in objectTypes)
                {
                    var instance = Activator.CreateInstance(item);

                    List<PropertyInfo> properties = new List<PropertyInfo>();
                    List<MethodInfo> methods = new List<MethodInfo>();

                    instance?.GetType().GetProperties().ToList().ForEach(x => properties.Add(x));
                    instance?.GetType()
                            .GetMethods()
                            .Where(x => x.GetCustomAttribute<ToExportAttribute>() != null&& x.GetCustomAttribute<HideFromExportAttribute>()!=null)//HideFromExportAttribute hozzáadása kétséges
                            .ToList()
                            .ForEach(x => methods.Add(x));
                    XElement entityNode = WriteToXML(item, properties, methods);
                    xdoc.Root!.Add(entityNode);
                }


                xdoc.Save("ExPoRtÁlTaM.xml");
            }
            private static XElement WriteToXML(Type objType, List<PropertyInfo> properties, List<MethodInfo> methods)
            {
                XElement entity = new XElement("entity");
                entity.Add(new XAttribute("hash", objType.Name.GetHashCode()));
                entity.Add(new XElement("type", objType.Name));
                entity.Add(new XElement("namespace", objType.Namespace));
                entity.Add(new XElement("properties", new XAttribute("count", properties.Count)));
                entity.Add(new XElement("methods", new XAttribute("count", methods.Count)));

                foreach (var item in properties)
                {
                    entity.Element("properties")?.Add(new XElement("property", item));
                }

                Func<MethodInfo, string> trimChars = x =>
                {
                    string temp = x.Name.TrimEnd(')');
                    temp = temp.TrimEnd('(');
                    return $"{x.ReturnParameter} {temp}";
                };

                foreach (var item in methods)
                {
                    entity.Element("methods")?.Add(new XElement("method", trimChars(item)));
                }

                return entity;
            }
        }
    
}

