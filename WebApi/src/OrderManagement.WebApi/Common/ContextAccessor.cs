using OrderManagement.Core.Abstractions;

namespace OrderManagement.WebApi.Common;

public class ContextAccessor
{
    private static readonly AsyncLocal<ContextHolder> Holder = new();

    public IContext? Context
    {
        get => Holder.Value?.Context;
        set
        {
            var holder = Holder.Value;
            if (holder != null)
            {
                holder.Context = null;
            }

            if (value != null)
            {
                Holder.Value = new ContextHolder { Context = value };
            }
        }
    }

    private class ContextHolder
    {
        public IContext? Context;
    }
}