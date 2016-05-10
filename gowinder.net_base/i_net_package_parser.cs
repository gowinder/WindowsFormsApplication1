// gowinder@hotmail.com
// gowinder.net_base
// i_net_package_parser.cs
// 2016-05-10-14:11

namespace gowinder.net_base
{
    public interface i_net_package_parser
    {
        net_package parse(object data, int offset, int length);
    }
}