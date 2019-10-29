using System.Collections.Generic;
using EPiServer.Security;
using EPiServer.Shell.Navigation;

namespace Foundation.Features.FoundationCal
{
    [MenuProvider]
    public class FoundationCalMenuProvider : IMenuProvider
    {
        public IEnumerable<MenuItem> GetMenuItems()
        {
            var toolbox = new SectionMenuItem("Brilliance", "/global/brilliance")
            {
                IsAvailable = (request) => PrincipalInfo.HasAdminAccess
            };

            var profiles = new UrlMenuItem("Foundation Cal", "/global/brilliance/foundcationcal", "/foundationcal/index")
            {
                
                IsAvailable = (request) => PrincipalInfo.HasAdminAccess,
            };

            return new MenuItem[] {toolbox,profiles};
        }
    }
}
