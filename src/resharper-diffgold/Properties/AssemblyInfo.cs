using System.Reflection;
using JetBrains.ActionManagement;
using JetBrains.Application.PluginSupport;

// General Information about an assembly is controlled through the following 
// set of attributes. Change these attribute values to modify the information
// associated with an assembly.
[assembly: AssemblyTitle("resharper-diffgold")]
[assembly: AssemblyDescription("Adds a button to the unit test sessions view to diff ReSharper gold files")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("Matt Ellis")]
[assembly: AssemblyProduct("resharper-diffgold")]
[assembly: AssemblyCopyright("Copyright © Matt Ellis, 2014")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]

[assembly: AssemblyVersion("1.0.0.0")]
[assembly: AssemblyFileVersion("1.0.0.0")]

[assembly: ActionsXml("resharper-diffgold.Actions.xml")]

// The following information is displayed by ReSharper in the Plugins dialog
[assembly: PluginTitle("DiffGold")]
[assembly: PluginDescription("Adds a button to the unit test sessions view to diff ReSharper gold files")]
[assembly: PluginVendor("Matt Ellis")]
