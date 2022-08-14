using System;

namespace Quiz_App
{
    public interface IPage
    {
        event EventHandler<IPage> PageChanged;
    }
}