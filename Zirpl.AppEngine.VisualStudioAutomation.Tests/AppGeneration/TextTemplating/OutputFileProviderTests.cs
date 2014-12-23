using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;
using Telerik.JustMock.Helpers;
using Zirpl.AppEngine.VisualStudioAutomation.AppGeneration.TextTemplating;
using Zirpl.Reflection;
using Zirpl.AppEngine.VisualStudioAutomation.AppGeneration.Config;
using Zirpl.AppEngine.VisualStudioAutomation.TextTemplating;

namespace Zirpl.AppEngine.VisualStudioAutomation.Tests.AppGeneration.TextTemplating
{
    [TestFixture]
    public class OutputFileProviderTests
    {
        [Test]
        public void TestGetFileName_OncePerApp()
        {
            new OutputInfoProvider().Access().Invoke<String>("GetFileName", "_", null).Should().Be("_.cs");
            new OutputInfoProvider().Access().Invoke<String>("GetFileName", "__", null).Should().Be("__.cs");
            new OutputInfoProvider().Access().Invoke<String>("GetFileName", "___", null).Should().Be("___.cs");
            new OutputInfoProvider().Access().Invoke<String>("GetFileName", "Service", null).Should().Be("Service.cs");
            new OutputInfoProvider().Access().Invoke<String>("GetFileName", "_Service", null).Should().Be("_Service.cs");
            new OutputInfoProvider().Access().Invoke<String>("GetFileName", "__Service", null).Should().Be("__Service.cs");
            new OutputInfoProvider().Access().Invoke<String>("GetFileName", "_Service_", null).Should().Be("_Service_.cs");
            new OutputInfoProvider().Access().Invoke<String>("GetFileName", "Service_", null).Should().Be("Service_.cs");
            new OutputInfoProvider().Access().Invoke<String>("GetFileName", "Service__", null).Should().Be("Service__.cs");
            new OutputInfoProvider().Access().Invoke<String>("GetFileName", "Service_txt", null).Should().Be("Service.txt");
            new OutputInfoProvider().Access().Invoke<String>("GetFileName", "Service__txt", null).Should().Be("Service_.txt");
            new OutputInfoProvider().Access().Invoke<String>("GetFileName", "Service_txt_", null).Should().Be("Service_txt_.cs");
            new OutputInfoProvider().Access().Invoke<String>("GetFileName", "dt", null).Should().Be("dt.cs");
            new OutputInfoProvider().Access().Invoke<String>("GetFileName", "dt_ext", null).Should().Be("dt.ext");
            new OutputInfoProvider().Access().Invoke<String>("GetFileName", "Service_dt_ext", null).Should().Be("Service_dt.ext");
            new OutputInfoProvider().Access().Invoke<String>("GetFileName", "Service_dt", null).Should().Be("Service.dt");
        }

        [Test]
        public void TestGetFileName_OncePerDomainType()
        {
            new OutputInfoProvider().Access().Invoke<String>("GetFileName", "dT", "Address").Should().Be("Address.cs");
            new OutputInfoProvider().Access().Invoke<String>("GetFileName", "_dT", "Address").Should().Be("Address.cs");
            new OutputInfoProvider().Access().Invoke<String>("GetFileName", "__dT", "Address").Should().Be("_Address.cs");
            new OutputInfoProvider().Access().Invoke<String>("GetFileName", "_dT_", "Address").Should().Be("Address.cs");
            new OutputInfoProvider().Access().Invoke<String>("GetFileName", "__dT__", "Address").Should().Be("_Address_.cs");
            new OutputInfoProvider().Access().Invoke<String>("GetFileName", "dT_", "Address").Should().Be("Address.cs");
            new OutputInfoProvider().Access().Invoke<String>("GetFileName", "dT__", "Address").Should().Be("Address_.cs");
            new OutputInfoProvider().Access().Invoke<String>("GetFileName", "dT_txt", "Address").Should().Be("Address.txt");
            new OutputInfoProvider().Access().Invoke<String>("GetFileName", "dT__txt", "Address").Should().Be("Address.txt");
            new OutputInfoProvider().Access().Invoke<String>("GetFileName", "dT___txt", "Address").Should().Be("Address_.txt");
            new OutputInfoProvider().Access().Invoke<String>("GetFileName", "_dT_txt", "Address").Should().Be("Address.txt");
            new OutputInfoProvider().Access().Invoke<String>("GetFileName", "__dT_txt", "Address").Should().Be("_Address.txt");
            new OutputInfoProvider().Access().Invoke<String>("GetFileName", "_dT__txt", "Address").Should().Be("Address.txt");
            new OutputInfoProvider().Access().Invoke<String>("GetFileName", "__dT__txt", "Address").Should().Be("_Address.txt");
            new OutputInfoProvider().Access().Invoke<String>("GetFileName", "_dT___txt", "Address").Should().Be("Address_.txt");
            new OutputInfoProvider().Access().Invoke<String>("GetFileName", "__dT___txt", "Address").Should().Be("_Address_.txt");
            new OutputInfoProvider().Access().Invoke<String>("GetFileName", "dT_Service", "Address").Should().Be("Address.Service");
            new OutputInfoProvider().Access().Invoke<String>("GetFileName", "dT_Service_cs", "Address").Should().Be("AddressService.cs");

            #region Exceptions
            new Action(() =>{new OutputInfoProvider().Access().Invoke<String>("GetFileName", "_", "Address");}).ShouldThrow<Exception>();
            new Action(() =>{new OutputInfoProvider().Access().Invoke<String>("GetFileName", "__", "Address");}).ShouldThrow<Exception>();
            new Action(() =>{new OutputInfoProvider().Access().Invoke<String>("GetFileName", "___", "Address");}).ShouldThrow<Exception>();
            new Action(() =>{new OutputInfoProvider().Access().Invoke<String>("GetFileName", "Service", "Address");}).ShouldThrow<Exception>();
            new Action(() =>{new OutputInfoProvider().Access().Invoke<String>("GetFileName", "_Service", "Address");}).ShouldThrow<Exception>();
            new Action(() =>{new OutputInfoProvider().Access().Invoke<String>("GetFileName", "__Service", "Address");}).ShouldThrow<Exception>();
            new Action(() =>{new OutputInfoProvider().Access().Invoke<String>("GetFileName", "_Service_", "Address");}).ShouldThrow<Exception>();
            new Action(() =>{new OutputInfoProvider().Access().Invoke<String>("GetFileName", "Service_", "Address");}).ShouldThrow<Exception>();
            new Action(() =>{new OutputInfoProvider().Access().Invoke<String>("GetFileName", "Service__", "Address");}).ShouldThrow<Exception>();
            new Action(() =>{new OutputInfoProvider().Access().Invoke<String>("GetFileName", "Service_txt", "Address");}).ShouldThrow<Exception>();
            new Action(() =>{new OutputInfoProvider().Access().Invoke<String>("GetFileName", "Service__txt", "Address");}).ShouldThrow<Exception>();
            new Action(() =>{new OutputInfoProvider().Access().Invoke<String>("GetFileName", "Service_txt_", "Address");}).ShouldThrow<Exception>();
            #endregion
        }

    }
}
