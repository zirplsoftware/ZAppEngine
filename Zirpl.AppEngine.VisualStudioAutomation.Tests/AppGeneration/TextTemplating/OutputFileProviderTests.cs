using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;
using Zirpl.AppEngine.VisualStudioAutomation.AppGeneration.TextTemplating;
using Zirpl.Reflection;
using Zirpl.AppEngine.VisualStudioAutomation.AppGeneration.Config;

namespace Zirpl.AppEngine.VisualStudioAutomation.Tests.AppGeneration.TextTemplating
{
    [TestFixture]
    public class OutputFileProviderTests
    {
        public class OncePerAppTypes : OncePerAppTemplate
        {
            public OncePerAppTypes()
            {
                this.Session = new ConcurrentDictionary<string, object>();
                this.Session.Add("App", null);
            }
            public class _ : OncePerAppTypes
            {
            }
            public class __ : OncePerAppTypes
            {
            }
            public class ___ : OncePerAppTypes
            {
            }
            public class Service : OncePerAppTypes
            {
            }
            public class _Service : OncePerAppTypes
            {
            }
            public class __Service : OncePerAppTypes
            {
            }
            public class _Service_ : OncePerAppTypes
            {
            }
            public class Service_ : OncePerAppTypes
            {
            }
            public class Service__ : OncePerAppTypes
            {
            }
            public class Service_txt : OncePerAppTypes
            {
            }
            public class Service__txt : OncePerAppTypes
            {
            }
            public class Service_txt_ : OncePerAppTypes
            {
            }
            public class dt : OncePerAppTypes
            {
            }
            public class dt_ext : OncePerAppTypes
            {
            }
            public class Service_dt_ext : OncePerAppTypes
            { 
            }
            public class Service_dt : OncePerAppTypes
            {
            }

            public override string TransformText()
            {
                return null;
            }
        }
        
        [Test]
        public void TestGetFileName_OncePerApp()
        {
            new OutputFileProvider(new OncePerAppTypes._()).GetAccessor().InvokeMethod<String>("GetFileName").Should().Be("_.cs");
            new OutputFileProvider(new OncePerAppTypes.__()).GetAccessor().InvokeMethod<String>("GetFileName").Should().Be("__.cs");
            new OutputFileProvider(new OncePerAppTypes.___()).GetAccessor().InvokeMethod<String>("GetFileName").Should().Be("___.cs");
            new OutputFileProvider(new OncePerAppTypes.Service()).GetAccessor().InvokeMethod<String>("GetFileName").Should().Be("Service.cs");
            new OutputFileProvider(new OncePerAppTypes._Service()).GetAccessor().InvokeMethod<String>("GetFileName").Should().Be("_Service.cs");
            new OutputFileProvider(new OncePerAppTypes.__Service()).GetAccessor().InvokeMethod<String>("GetFileName").Should().Be("__Service.cs");
            new OutputFileProvider(new OncePerAppTypes._Service_()).GetAccessor().InvokeMethod<String>("GetFileName").Should().Be("_Service_.cs");
            new OutputFileProvider(new OncePerAppTypes.Service_()).GetAccessor().InvokeMethod<String>("GetFileName").Should().Be("Service_.cs");
            new OutputFileProvider(new OncePerAppTypes.Service__()).GetAccessor().InvokeMethod<String>("GetFileName").Should().Be("Service__.cs");
            new OutputFileProvider(new OncePerAppTypes.Service_txt()).GetAccessor().InvokeMethod<String>("GetFileName").Should().Be("Service.txt");
            new OutputFileProvider(new OncePerAppTypes.Service__txt()).GetAccessor().InvokeMethod<String>("GetFileName").Should().Be("Service_.txt");
            new OutputFileProvider(new OncePerAppTypes.Service_txt_()).GetAccessor().InvokeMethod<String>("GetFileName").Should().Be("Service_txt_.cs");
            new OutputFileProvider(new OncePerAppTypes.dt()).GetAccessor().InvokeMethod<String>("GetFileName").Should().Be("dt.cs");
            new OutputFileProvider(new OncePerAppTypes.dt_ext()).GetAccessor().InvokeMethod<String>("GetFileName").Should().Be("dt.ext");
            new OutputFileProvider(new OncePerAppTypes.Service_dt_ext()).GetAccessor().InvokeMethod<String>("GetFileName").Should().Be("Service_dt.ext");
            new OutputFileProvider(new OncePerAppTypes.Service_dt()).GetAccessor().InvokeMethod<String>("GetFileName").Should().Be("Service.dt");
        }

        public class OncePerDomainTypeTypes : OncePerDomainTypeTemplate
        {
            public override string TransformText()
            {
                return null;
            }
            public OncePerDomainTypeTypes()
            {
                this.Session = new ConcurrentDictionary<string, object>();
                this.Session.Add("App", null);
                this.Session.Add("DomainType", new DomainType() { Name = "Address"});
            }

            #region Exception-producing types
            public class _ : OncePerDomainTypeTypes
            {
            }
            public class __ : OncePerDomainTypeTypes
            {
            }
            public class ___ : OncePerDomainTypeTypes
            {
            }
            public class Service : OncePerDomainTypeTypes
            {
            }
            public class _Service : OncePerDomainTypeTypes
            {
            }
            public class __Service : OncePerDomainTypeTypes
            {
            }
            public class _Service_ : OncePerDomainTypeTypes
            {
            }
            public class Service_ : OncePerDomainTypeTypes
            {
            }
            public class Service__ : OncePerDomainTypeTypes
            {
            }
            public class Service_txt : OncePerDomainTypeTypes
            {
            }
            public class Service__txt : OncePerDomainTypeTypes
            {
            }
            public class Service_txt_ : OncePerDomainTypeTypes
            {
            }
            #endregion

            public class dT : OncePerDomainTypeTypes
            {
            }
            public class _dT : OncePerDomainTypeTypes
            {
            }
            public class __dT : OncePerDomainTypeTypes
            {
            }
            public class _dT_ : OncePerDomainTypeTypes
            {
            }
            public class __dT__ : OncePerDomainTypeTypes
            {
            }
            public class dT_ : OncePerDomainTypeTypes
            {
            }
            public class dT__ : OncePerDomainTypeTypes
            {
            }
            public class dT_txt : OncePerDomainTypeTypes
            {
            }
            public class dT__txt : OncePerDomainTypeTypes
            {
            }
            public class dT___txt : OncePerDomainTypeTypes
            {
            }

            public class _dT_txt : OncePerDomainTypeTypes
            {
            }
            public class __dT_txt : OncePerDomainTypeTypes
            {
            }
            public class _dT__txt : OncePerDomainTypeTypes
            {
            }
            public class __dT__txt : OncePerDomainTypeTypes
            {
            }
            public class _dT___txt : OncePerDomainTypeTypes
            {
            }
            public class __dT___txt : OncePerDomainTypeTypes
            {
            }
            public class dT_Service : OncePerDomainTypeTypes
            {
            }
            public class dT_Service_cs : OncePerDomainTypeTypes
            {
            }

        }

        [Test]
        public void TestGetFileName_OncePerDomainType()
        {
            new OutputFileProvider(new OncePerDomainTypeTypes.dT()).GetAccessor().InvokeMethod<String>("GetFileName").Should().Be("Address.cs");
            new OutputFileProvider(new OncePerDomainTypeTypes._dT()).GetAccessor().InvokeMethod<String>("GetFileName").Should().Be("Address.cs");
            new OutputFileProvider(new OncePerDomainTypeTypes.__dT()).GetAccessor().InvokeMethod<String>("GetFileName").Should().Be("_Address.cs");
            new OutputFileProvider(new OncePerDomainTypeTypes._dT_()).GetAccessor().InvokeMethod<String>("GetFileName").Should().Be("Address.cs");
            new OutputFileProvider(new OncePerDomainTypeTypes.__dT__()).GetAccessor().InvokeMethod<String>("GetFileName").Should().Be("_Address_.cs");
            new OutputFileProvider(new OncePerDomainTypeTypes.dT_()).GetAccessor().InvokeMethod<String>("GetFileName").Should().Be("Address.cs");
            new OutputFileProvider(new OncePerDomainTypeTypes.dT__()).GetAccessor().InvokeMethod<String>("GetFileName").Should().Be("Address_.cs");
            new OutputFileProvider(new OncePerDomainTypeTypes.dT_txt()).GetAccessor().InvokeMethod<String>("GetFileName").Should().Be("Address.txt");
            new OutputFileProvider(new OncePerDomainTypeTypes.dT__txt()).GetAccessor().InvokeMethod<String>("GetFileName").Should().Be("Address.txt");
            new OutputFileProvider(new OncePerDomainTypeTypes.dT___txt()).GetAccessor().InvokeMethod<String>("GetFileName").Should().Be("Address_.txt");
            new OutputFileProvider(new OncePerDomainTypeTypes._dT_txt()).GetAccessor().InvokeMethod<String>("GetFileName").Should().Be("Address.txt");
            new OutputFileProvider(new OncePerDomainTypeTypes.__dT_txt()).GetAccessor().InvokeMethod<String>("GetFileName").Should().Be("_Address.txt");
            new OutputFileProvider(new OncePerDomainTypeTypes._dT__txt()).GetAccessor().InvokeMethod<String>("GetFileName").Should().Be("Address.txt");
            new OutputFileProvider(new OncePerDomainTypeTypes.__dT__txt()).GetAccessor().InvokeMethod<String>("GetFileName").Should().Be("_Address.txt");
            new OutputFileProvider(new OncePerDomainTypeTypes._dT___txt()).GetAccessor().InvokeMethod<String>("GetFileName").Should().Be("Address_.txt");
            new OutputFileProvider(new OncePerDomainTypeTypes.__dT___txt()).GetAccessor().InvokeMethod<String>("GetFileName").Should().Be("_Address_.txt");
            new OutputFileProvider(new OncePerDomainTypeTypes.dT_Service()).GetAccessor().InvokeMethod<String>("GetFileName").Should().Be("Address.Service");
            new OutputFileProvider(new OncePerDomainTypeTypes.dT_Service_cs()).GetAccessor().InvokeMethod<String>("GetFileName").Should().Be("AddressService.cs");

            #region Exceptions
            AssertThrowsException<Exception>(() =>
            {
                new OutputFileProvider(new OncePerDomainTypeTypes._()).GetAccessor().InvokeMethod<String>("GetFileName");
            }); 
            AssertThrowsException<Exception>(() =>
            {
                new OutputFileProvider(new OncePerDomainTypeTypes.__()).GetAccessor().InvokeMethod<String>("GetFileName");
            });
            AssertThrowsException<Exception>(() =>
            {
                new OutputFileProvider(new OncePerDomainTypeTypes.___()).GetAccessor().InvokeMethod<String>("GetFileName");
            });
            AssertThrowsException<Exception>(() =>
            {
                new OutputFileProvider(new OncePerDomainTypeTypes.Service()).GetAccessor().InvokeMethod<String>("GetFileName");
            });
            AssertThrowsException<Exception>(() =>
            {
                new OutputFileProvider(new OncePerDomainTypeTypes._Service()).GetAccessor().InvokeMethod<String>("GetFileName");
            });
            AssertThrowsException<Exception>(() =>
            {
                new OutputFileProvider(new OncePerDomainTypeTypes.__Service()).GetAccessor().InvokeMethod<String>("GetFileName");
            });
            AssertThrowsException<Exception>(() =>
            {
                new OutputFileProvider(new OncePerDomainTypeTypes._Service_()).GetAccessor().InvokeMethod<String>("GetFileName");
            });
            AssertThrowsException<Exception>(() =>
            {
                new OutputFileProvider(new OncePerDomainTypeTypes.Service_()).GetAccessor().InvokeMethod<String>("GetFileName");
            });
            AssertThrowsException<Exception>(() =>
            {
                new OutputFileProvider(new OncePerDomainTypeTypes.Service__()).GetAccessor().InvokeMethod<String>("GetFileName");
            });
            AssertThrowsException<Exception>(() =>
            {
                new OutputFileProvider(new OncePerDomainTypeTypes.Service_txt()).GetAccessor().InvokeMethod<String>("GetFileName");
            });
            AssertThrowsException<Exception>(() =>
            {
                new OutputFileProvider(new OncePerDomainTypeTypes.Service__txt()).GetAccessor().InvokeMethod<String>("GetFileName");
            });
            AssertThrowsException<Exception>(() =>
            {
                new OutputFileProvider(new OncePerDomainTypeTypes.Service_txt_()).GetAccessor().InvokeMethod<String>("GetFileName");
            });
            #endregion
        }

        private TException AssertThrowsException<TException>(Action action) where TException : Exception
        {
            Exception exceptionThrown = null;
            try
            {
                action();
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null
                    && ex.InnerException is TException
                    && ex.InnerException.Message.StartsWith("A OncePerDomainTypeTemplate has been named without a DomainType replacement token (_dt_, _dt, or dt_): "))
                {
                    exceptionThrown = ex;
                }
                else
                {
                    throw new Exception("Exception thrown but not of correct type: " + ex.GetType().Name);
                }
            }

            if (exceptionThrown == null)
            {
                throw new Exception("Exception not thrown");
            }

            return (TException)exceptionThrown;
        }
    }
}
