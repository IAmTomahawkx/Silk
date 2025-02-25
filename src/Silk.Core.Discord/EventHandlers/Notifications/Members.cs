﻿using MediatR;

namespace Silk.Core.Discord.EventHandlers.Notifications
{
    public class MemberJoined : INotification { }

    public class MemberRemoved : INotification { }

    public class MemberUpdated : INotification { }

    public class MemberStatusUpdated : INotification { }

}