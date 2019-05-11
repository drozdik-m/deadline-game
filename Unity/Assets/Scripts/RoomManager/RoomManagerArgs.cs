
public class RoomManagerArgs
{
    public RoomList CurrentRoom
    {
        get
        {
            return currentRoom;
        }
    }

    private readonly RoomList currentRoom;

    public RoomManagerArgs(RoomList currentRoom)
    {
        this.currentRoom = currentRoom;
    }
}
