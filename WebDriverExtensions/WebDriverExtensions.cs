using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace JimmyJohnsAutomation.WebDriverExtensions
{
    public static class WebDriverExtensions
    {
        public static void EnterText(this IWebElement element, string text, string elementName)
        {
            element.Clear();
            element.SendKeys(text);
            Console.WriteLine(text + " entered in the " + elementName + " field.");
        }
        public static bool IsDisplayed(this IWebElement element, string elementName)
        {
            bool result;
            try
            {
                result = element.Displayed;
                Console.WriteLine(elementName + " is Displayed.");
            }
            catch (Exception)
            {
                result = false;
                Console.WriteLine(elementName + " is not Displayed.");
            }
            return result;
        }
        public static void ClickOnIt(this IWebElement element, string elementName)
        {
            element.Click();
            Console.WriteLine("Clicked on " + elementName);
        }
        public static void SelectByText(this IWebElement element, string text, string elementName)
        {
            SelectElement oSelect = new SelectElement(element);
            oSelect.SelectByText(text);
            Console.WriteLine(text + " text selected on " + elementName);
        }
        public static void SelectByIndex(this IWebElement element, int index, string elementName)
        {
            SelectElement oSelect = new SelectElement(element);
            oSelect.SelectByIndex(index);
            Console.WriteLine(index + " index selected on " + elementName);
        }
        public static void SelectByValue(this IWebElement element, string text, string elementName)
        {
            SelectElement oSelect = new SelectElement(element);
            oSelect.SelectByValue(text);
            Console.WriteLine(text + " value selected on " + elementName);
        }

        /// <summary>
        /// Gets the text of a specified element
        /// </summary>
        /// <param name="element">The element to get the text of</param>
        /// <returns>The text of the element</returns>
        public static string GetElementText(IWebElement element)
        {
            var text = "";
            while (true)
            {
                try { text = element.Text; }
                catch (StaleElementReferenceException) { }
                return text;
            }
        }

        /// <summary>
        /// Gets the current state of a specified checkbox element
        /// </summary>
        /// <param name="element">The checkbox element to validate</param>
        /// <returns>True if the checkbox is checked, false if the checkbox can not be found or is unchecked</returns>
        public static bool GetCheckboxState(IWebElement element)
        {
            if (!element.Displayed) { return false; }
            if (!element.Enabled) { return false; }
            return element.Selected;
        }

        public static void DoubleClick(IWebElement element)
        {
            element.Click();
            element.Click();
        }

        public static string GetElementAttributeValue(IWebElement element, string value)
        {
            var text = "";
            while (true)
            {
                try { text = element.GetAttribute(value); }
                catch (StaleElementReferenceException) { }
                return text;
            }
        }
        public static string GetElementCssValue(IWebElement element, string value)
        {
            var text = "";
            while (true)
            {
                try { text = element.GetCssValue(value); }
                catch (StaleElementReferenceException) { }
                return text;
            }
        }
        public static void WaitUntil<T>(this IWebDriver webDriver, Func<IWebDriver, T> condition)
        {
            WaitUntil(webDriver, condition, Int32.Parse(ConfigurationManager.AppSettings["Timeout"]));
        }
        public static void WaitUntil<T>(this IWebDriver webDriver, Func<IWebDriver, T> condition,
        int waitTimeoutInSeconds)
        {
            var wait = new WebDriverWait(webDriver, new TimeSpan(0, 0, 0, waitTimeoutInSeconds));
            wait.Until(condition);
        }
        public static IWebElement WaitForElement(this IWebDriver webDriver, By by)
        {
            return WaitForElement(webDriver, by, Int32.Parse(ConfigurationManager.AppSettings["Timeout"]));
        }
        public static IWebElement WaitForElement(this IWebDriver webDriver, By by, int timeout)
        {
            try
            {
                webDriver.WaitUntil(x => x.FindElements(by).Count > 0, timeout);
                return webDriver.FindElement(by);
            }
            catch (StaleElementReferenceException)
            {
                return WaitForElement(webDriver, by, timeout);
            }
            catch (InvalidSelectorException e)
            {
                throw e;
            }
            catch (Exception)
            {
                string message =
                string.Format("WaitForElement failed after {0} seconds, element should be present : {1}", timeout,
                by);
                throw new NoSuchElementException(message);
            }
        }
        public static void WaitForElementToRemove(this IWebDriver webDriver, By by)
        {
            try
            {
                webDriver.WaitUntil(x => x.FindElements(by).Count == 0);
            }
            catch
            {
                string message =
                string.Format(
                "WaitForElementToRemove failed after {0} seconds, element should NOT be present : {1}",
                Int32.Parse(ConfigurationManager.AppSettings["Timeout"]), @by);
                //throw new InvalidElementStateException(string.Concat(ScenarioHooks.GetFunctionNameFromCallStack(),
                // message));
            }
        }
        public static void WaitForElementVisible(this IWebDriver webDriver, By by, bool visible = true, int timeout = -1)
        {
            if (timeout == -1)
                timeout = Int32.Parse(ConfigurationManager.AppSettings["Timeout"]);
            try
            {
                webDriver.WaitUntil(x => x.FindElement(by).Displayed == visible, timeout);
            }
            catch
            {
                var should = visible ? "should" : "should NOT";
                string message =
                string.Format("WaitForElementVisible failed after {0} seconds, element {2} be present : {1}",
                Int32.Parse(ConfigurationManager.AppSettings["Timeout"]), @by, should);
                //throw new InvalidElementStateException(string.Concat(ScenarioHooks.GetFunctionNameFromCallStack(),
                //message));
            }
        }
        public static void WaitForElementEnabled(this IWebDriver webDriver, By by, bool enabled = true, int timeout = -1)
        {
            if (timeout == -1)
                timeout = Int32.Parse(ConfigurationManager.AppSettings["Timeout"]);
            try
            {
                webDriver.WaitUntil(x => x.FindElement(by).Enabled == enabled, timeout);
            }
            catch
            {
                var should = enabled ? "should" : "should NOT";
                string message =
                string.Format("WaitForElementVisible failed after {0} seconds, element {2} be present : {1}",
                Int32.Parse(ConfigurationManager.AppSettings["Timeout"]), @by, should);
                //throw new InvalidElementStateException(string.Concat(ScenarioHooks.GetFunctionNameFromCallStack(),
                //message));
            }
        }
        public static void WaitForAndSelectFrame(this IWebDriver driver, String frameName, int timeout = 30)
        {
            driver.SwitchTo().DefaultContent();
            var startTime = DateTime.Now;
            while (!driver.FindAndSelectFrame(frameName))
            {
                Thread.Sleep(1000);
                if (DateTime.Now.Subtract(startTime).TotalSeconds > timeout)
                {
                    throw new TimeoutException("Timed out waiting to select frame " + frameName);
                }
            }
        }
        private static Boolean FindAndSelectFrame(this IWebDriver driver, string frameName)
        {
            var framePath = new List<String>();
            if (FindFrame(driver, frameName, framePath))
            {
                return FollowFramePath(driver, framePath);
            }
            return false;
        }
        private static Boolean FindFrame(IWebDriver driver, string frameName, List<string> framePath)
        {
            if (!FollowFramePath(driver, framePath))
            {
                // Could not switch to the frame
                return false;
            }
            var FrameIds = GetFrameIds(driver);
            if (FrameIds.Contains(frameName))
            {
                //we found the path, add it to the dictionary and return
                framePath.Add(frameName);
                return true;
            }
            foreach (string s in FrameIds)
            {
                //add the path and recurse
                framePath.Add(s);
                if (FindFrame(driver, frameName, framePath))
                {
                    return true;
                }
                //was not fruitful remove the path
                framePath.Remove(s);
            }
            //Could not find it this round 
            return false;
        }
        private static List<String> GetFrameIds(IWebDriver driver)
        {
            var FrameIds = new List<string>();
            string newId;
            string newName;
            string[] Tags = { "frame", "iframe" };

            foreach (string tag in Tags)
            {
                var ReturnElements = driver.FindElements(By.TagName(tag));
                foreach (var E in ReturnElements)
                {
                    //Get the ID or name of the frame or iframe
                    newId = E.GetAttribute("id");
                    if (!String.IsNullOrEmpty(newId))
                    {
                        FrameIds.Add(newId);
                    }
                    else
                    {
                        newName = E.GetAttribute("name");
                        if (!String.IsNullOrEmpty(newName))
                        {
                            FrameIds.Add(newName);
                        }
                    }
                }
            }
            return FrameIds;
        }
        private static Boolean FollowFramePath(IWebDriver driver, List<string> framePath)
        {
            try
            {
                driver.SwitchTo().DefaultContent();
                foreach (string s in framePath)
                {
                    driver.SwitchTo().Frame(s);
                }
                //if were here the path was followd
                return true;
            }
            catch
            {
                return false;
            }
        }
        public static void VerifyTextPresent(this IWebDriver webDriver, string text, bool visible = true)
        {
            if (webDriver.PageSource.Contains(text) == visible) return;
            var message = string.Format("Text {0} present on page : {1}", visible ? "not" : "is", text);
        }
        public static void JsClick(this IWebDriver driver, IWebElement elementToClick)
        {
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click();", elementToClick);
        }

        public static void MaximizePage(this IWebDriver webDriver)
        {
            webDriver.Manage().Window.Maximize();
        }
        public static void Sleep(this IWebDriver webDriver, int millisecondsTimeout)
        {
            Thread.Sleep(millisecondsTimeout);
        }
        public static void SetText(this IWebElement webElement, string value)
        {
            webElement.Clear();
            webElement.SendKeys(value);
        }
        public static IWebElement WaitForVisible(this IWebElement element, bool visible = true, int timeout = 0)
        {
            if (timeout == 0) timeout = Int32.Parse(ConfigurationManager.AppSettings["Timeout"]);
            for (var i = 0; ((i < timeout / 1000) && (element.Displayed != visible)); i++)
            {
                Thread.Sleep(1000);
            }
            return element;
        }
        public static IWebElement Get(this List<IWebElement> webElementList, int index)
        {
            return webElementList[index];
        }
        public static string GetHiddenField(this IWebDriver webDriver, string hiddenFieldId)
        {
            var script = string.Format("return $('input#{0}:hidden').val();", hiddenFieldId);
            return ((IJavaScriptExecutor)webDriver).ExecuteScript(script).ToString();
        }
        public static void SetHiddenValue(this IWebDriver webDriver, string hiddenId, string value)
        {
            var script = string.Format("$('input#{0}:hidden').val('{1}');", hiddenId, value);
            ((IJavaScriptExecutor)webDriver).ExecuteScript(script);
        }
        public static IWebElement FindVisibleElement(this IWebDriver webDriver, By by, bool retryOnceIfStale = false)
        {
            try
            {
                var elements = webDriver.FindElements(by);
                foreach (IWebElement element in elements)
                {
                    if (element.Displayed) return element;
                }
                throw new ElementNotVisibleException("Could not find visible element with : " + @by);
            }
            catch (StaleElementReferenceException e)
            {
                if (retryOnceIfStale)
                {
                    Thread.Sleep(1500);
                    return webDriver.FindVisibleElement(by, false);
                }
                throw e;
            }
            throw new ElementNotVisibleException("Could not find visible element with : " + @by);
        }
        public static IWebElement FindNullableElement(this IWebDriver webDriver, By by)
        {
            var iteseconds = webDriver.FindElements(by);
            return iteseconds.Count > 0 ? iteseconds[0] : null;
        }
        public static IWebElement FindNullableElement(this IWebElement webElement, By by)
        {
            var iteseconds = webElement.FindElements(by);
            return iteseconds.Count > 0 ? iteseconds[0] : null;
        }
        public static bool IsElementPresent(this IWebDriver webDriver, By by)
        {
            return webDriver.FindNullableElement(by) != null;
        }
        public static bool IsElementVisible(this IWebDriver webDriver, By by)
        {
            try
            {
                var element = webDriver.FindNullableElement(by);
                var displayed = false;
                if (element != null)
                    displayed = element.Displayed;
                return displayed;
            }
            catch (StaleElementReferenceException)
            {
                return false;
            }
            catch (InvalidOperationException e)
            {
                if (e.Message.ToUpper().Contains("DETERMINING IF ELEMENT IS DISPLAYED"))
                {
                    return false;
                }
                throw e;
            }
        }
        public static void SetCheckbox(this IWebElement element, bool check)
        {
            if (element.Selected != check)
            {
                element.Click();
            }
        }
        public static void SelectByText(this IWebElement element, string text)
        {
            var options = element.FindElements(By.TagName("option"));
            foreach (var option in options)
            {
                bool isSelected = option.GetAttribute("selected") != null && option.GetAttribute("selected").ToLower() == "true";
                bool isMatch = option.Text.Contains(text);
                //Select the correct match and deselect all others
                if (isMatch != isSelected)
                    option.Click();
            }
        }
        public static void SelectByExactText(this IWebElement element, string text)
        {
            var options = element.FindElements(By.TagName("option"));
            foreach (var option in options)
            {
                bool isSelected = option.GetAttribute("selected") != null && option.GetAttribute("selected").ToLower() == "true";
                bool isMatch = option.Text == text;
                //Select the correct match and deselect all others
                if (isMatch != isSelected)
                    option.Click();
            }
        }

        public static void SelectNewWindow(this IWebDriver driver)
        {
            var currentId = driver.CurrentWindowHandle;
            foreach (var windowId in driver.WindowHandles.Where(windowId => currentId != windowId))
            {
                driver.SwitchTo().Window(windowId);
            }
        }

        public static bool IsWindowTitleVisible(this IWebDriver driver, string expectedBrowserTitle, bool usePatternMatch = false, bool displayed = true)
        {
            Regex regex = new Regex(expectedBrowserTitle);
            Match match = null;
            foreach (var windowId in driver.WindowHandles)
            {
                match = regex.Match(driver.Title);
                if (usePatternMatch == false)
                {
                    if ((displayed == true && driver.Title == expectedBrowserTitle) || (displayed == false && driver.Title != expectedBrowserTitle))
                        return true;
                }
                else if (usePatternMatch == true)
                {
                    if ((displayed == true && match.Success) || (displayed == false && !match.Success))
                        return true;
                }
            }
            return false;
        }
        public static void SelectWindowByTitle(this IWebDriver driver, string expectedBrowserTitle, bool usePatternMatch = false)
        {
            string currentId = null;
            try
            {
                currentId = driver.CurrentWindowHandle;
            }
            catch (NoSuchWindowException)
            {
            }
            List<string> FoundWindowTitles = new List<string>();
            Regex regex = new Regex(expectedBrowserTitle);
            Match match = null;
            foreach (var windowId in driver.WindowHandles)
            {
                driver.SwitchTo().Window(windowId);
                FoundWindowTitles.Add(driver.Title);
                match = regex.Match(driver.Title);
                if (usePatternMatch == false && driver.Title == expectedBrowserTitle)
                    break;
                else if (usePatternMatch == true && match.Success)
                    break;
            }
            if ((!usePatternMatch && driver.Title == expectedBrowserTitle) || (usePatternMatch && !match.Success))
            {
                if (currentId != null)
                    driver.SwitchTo().Window(currentId);
                throw new NoSuchWindowException("Could not find the window \"" + (usePatternMatch ? "matching the regular expression " : "") + expectedBrowserTitle + "\". Found only the windows titled: " + String.Join(", ", FoundWindowTitles.ToArray()));
            }
        }

        public static void clearBrowserCache(this IWebDriver driver)
        {
            driver.Manage().Cookies.DeleteAllCookies();
            driver.Navigate().Refresh();
        }
        public static void refreshBrowser(this IWebDriver driver)
        {
            driver.Navigate().Refresh();
        }
        public static void DeleteCookies(this IWebDriver driver)
        {
            driver.Manage().Cookies.DeleteAllCookies();
        }
        public static void DeleteIECookiesAndData()
        {
            Process p = new Process();
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.FileName = "RunDll32.exe";
            p.StartInfo.Arguments = "InetCpl.cpl,ClearMyTracksByProcess 2";
            p.Start();
            p.StandardOutput.ReadToEnd();
            p.WaitForExit();
        }

    }
}
