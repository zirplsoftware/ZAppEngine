﻿namespace Zirpl.AppEngine.Model
{
    public interface IVersionable
    {
        byte[] RowVersion { get; set; }
        bool IsRowVersionUsed { get; }
    }
}
