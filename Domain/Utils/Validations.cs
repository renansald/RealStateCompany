using Domain.Exceptions;

namespace Domain.Utils;

public static class Validations
{
    public static void ValidateId(int id)
    {
        if (id <= 0)
        {
            throw new BadRequestException("Invalid Id");
        }
    }

    public static void ValidateUpdate(int id, int objectId)
    {
        if (id <= 0 || id != objectId)
        {
            throw new BadRequestException("Invalid Id");
        } 
    }
}