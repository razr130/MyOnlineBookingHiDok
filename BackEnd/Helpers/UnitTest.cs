using System;
using System.Reflection;

/// <summary>
/// Detect if we are running as part of a nUnit unit test.
/// This is DIRTY and should only be used if absolutely necessary 
/// as its usually a sign of bad design.
/// </summary>    
/// 
namespace MyOnlineBooking.Helpers
{
    public static class UnitTestDetector
    {
        public static bool IsRunningFromMSTestV2()
        {
            foreach (Assembly assem in AppDomain.CurrentDomain.GetAssemblies())
            {
                string assemName = assem.FullName.ToLowerInvariant();
                if (assemName.StartsWith("microsoft.visualstudio.testplatform"))
                {
                    return true;
                }
            }
            return false;
        }
    }
}

