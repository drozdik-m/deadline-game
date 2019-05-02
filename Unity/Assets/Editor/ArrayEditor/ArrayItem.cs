using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrayItem
{
    private bool ValidateWholeName(string wholeName)
    {
        throw new NotImplementedException();
    }

    private Type GetTypeFromWholeName()
    {
        throw new NotImplementedException();
    }

    private List<int> GetPositionsFromWholeName()
    {
        throw new NotImplementedException();
    }

    private string GetUserNameFromWholeName()
    {
        throw new NotImplementedException();
    }

    private bool ValidateUserName(string userName)
    {
        throw new NotImplementedException();
    }

    private bool ValidatePositions(List<int> positions)
    {
        throw new NotImplementedException();
    }

    private bool ValidateType(Type type)
    {
        throw new NotImplementedException();
    }

    private string GenerateSerializedName()
    {
        throw new NotImplementedException();
    }

    public string wholeName;
    public string userName;
    public List<int> positions;
    public Type type;

    public ArrayItem(string wholeName)
    {
        this.wholeName = wholeName;

        if (!ValidateWholeName(wholeName))
            throw new ArrayItemException("Array item string name is not in correct format, " + wholeName);

        userName = GetUserNameFromWholeName();
        positions = GetPositionsFromWholeName();
        type = GetTypeFromWholeName();
    }

    public ArrayItem(string userName, List<int> positions, Type type)
    {
        if (!ValidateUserName(userName))
            throw new ArrayItemException("todo");

        if (!ValidatePositions(positions))
            throw new ArrayItemException("todo");

        if (!ValidateType(type))
            throw new ArrayItemException("todo");

        this.userName = userName;
        this.positions = positions;
        this.type = type;

        wholeName = GenerateSerializedName();
    }
}

public class ArrayItemException : Exception
{
    public ArrayItemException(string message)
        : base (message)
    {
    }
}
