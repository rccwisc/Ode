using Microsoft.AspNetCore.Mvc;

namespace OdeToFood.Controllers
{
    // remember "Controller" is assumed in the url
    //[Route("about")]
    //[Route("company[controller]/[action]")]
    [Route("[controller]/[action]")]
    public class AboutController
    {
        [Route("")]
        public string Phone()
        {
            return "1+608+555+1212";
        }

        //[Route("address")]
        //[Route("[action]")]
        public string Address()
        {
            return "6806 village park dr";
        }
    }
}
