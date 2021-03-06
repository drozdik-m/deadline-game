using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class ArrayItem
{
    public const int USERNAME_LEN = 128;

    public static Type MyGetType(string TypeName)
    {

        // Try Type.GetType() first. This will work with types defined
        // by the Mono runtime, in the same assembly as the caller, etc.
        var type = Type.GetType(TypeName);

        // If it worked, then we're done here
        if (type != null)
            return type;

        // If the TypeName is a full name, then we can try loading the defining assembly directly
        if (TypeName.Contains("."))
        {

            // Get the name of the assembly (Assumption is that we are using 
            // fully-qualified type names)
            var assemblyName = TypeName.Substring(0, TypeName.IndexOf('.'));

            // Attempt to load the indicated Assembly
            var assembly = Assembly.Load(assemblyName);
            if (assembly == null)
                return null;

            // Ask that assembly to return the proper Type
            type = assembly.GetType(TypeName);
            if (type != null)
                return type;

        }

        // If we still haven't found the proper type, we can enumerate all of the 
        // loaded assemblies and see if any of them define the type
        var currentAssembly = Assembly.GetExecutingAssembly();
        var referencedAssemblies = currentAssembly.GetReferencedAssemblies();
        foreach (var assemblyName in referencedAssemblies)
        {

            // Load the referenced assembly
            var assembly = Assembly.Load(assemblyName);
            if (assembly != null)
            {
                // See if that assembly defines the named type
                type = assembly.GetType(TypeName);
                if (type != null)
                    return type;
            }
        }

        // The type just couldn't be found...
        return null;

    }

    private string[] GetNamePieces(string wholeName)
    {
        string[] wholeNamePieces = wholeName.Split(' ');

        if (wholeNamePieces.Length != 3)
            throw new EditorException("Whole name of the item is not valid -> too much pieces after divison by space");

        wholeNamePieces[1] = wholeNamePieces[1].Substring(1, wholeNamePieces[1].Length - 2);

        return wholeNamePieces;
    }

    private Type GetTypeFromWholeName()
    {
        string[] wholeNamePieces = GetNamePieces(wholeName);
        return MyGetType(wholeNamePieces[1]);
    }

    private List<int> GetPositionsFromWholeName()
    {
        string[] wholeNamePieces = GetNamePieces(wholeName);
        string positionsStr = wholeNamePieces[0].Substring(1, wholeNamePieces[0].Length - 1);
        string[] positionsStrArr = positionsStr.Split(',');

        List<int> parsedPositions = new List<int>();
        foreach (string posStr in positionsStrArr)
            parsedPositions.Add(int.Parse(posStr));

        return parsedPositions;
    }

    private string GetUserNameFromWholeName()
    {
        string[] wholeNamePieces = GetNamePieces(wholeName);
        return wholeNamePieces[2];
    }

    private bool ValidateWholeName(string wholeName)
    {
        // i am really sorry, but I am right now not capable of doing regular expressions
        // i hope you understand and accept this awful if, if, if, ... if solution =)

        // null or white space -> false
        if (String.IsNullOrWhiteSpace(wholeName)) return false;

        // does not start with # -> false
        if (!wholeName.StartsWith("#")) return false;

        
        string[] wholeNamePieces = GetNamePieces(wholeName);

        // get rid of first '#' in first piece
        wholeNamePieces[0] = wholeNamePieces[0].Substring(1, wholeNamePieces[0].Length - 1);

        string[] positions = wholeNamePieces[0].Split(',');

        // no positions -> false
        if (positions.Length < 1) return false;

        // try parse all positions, if some can't be parsed -> false
        List<int> positionsToTest = new List<int>();
        foreach (string posStr in positions)
        {
            if (!int.TryParse(posStr, out int res)) return false;
            else positionsToTest.Add(res);
        }

        // validate positions
        if (!ValidatePositions(positionsToTest)) return false;
            
        // validate type
        try
        {
            Type type = Type.GetType(wholeNamePieces[1]);
        }
        catch (Exception e)
        {
            Debug.Log("Type could not be created by string: " + e.Message);
            return false;
        }

        // validate user name
        if (!ValidateUserName(wholeNamePieces[2])) return false;

        return true;
    }

    private bool ValidateUserName(string userName)
    {
        if (String.IsNullOrWhiteSpace(userName)) return false;
        return userName.Length < USERNAME_LEN;
    }

    private bool ValidatePositions(List<int> positions)
    {
        // check duplicates && negative numbers
        foreach (int pos in positions)
        {
            // if negative, false
            if (pos < 0) return false;

            // if duplicate, false
            List<int> positionsToCheck = new List<int>(positions);
            positionsToCheck.Remove(pos);
            foreach (int pos2 in positionsToCheck)
                if (pos == pos2) return false;
        }

        return true;
    }

    private bool ValidateType(Type type)
    {
        return type != null;
    }

    public string wholeName;
    public string userName;
    public List<int> positions;
    public Type type;

    public ArrayItem(string wholeName)
    {
        this.wholeName = wholeName;

        if (!ValidateWholeName(wholeName))
            throw new EditorException("Array item string name is not in correct format, " + wholeName);

        userName = GetUserNameFromWholeName();
        positions = GetPositionsFromWholeName();
        type = GetTypeFromWholeName();
    }

    public ArrayItem(string userName, List<int> positions, Type type)
    {
        if (!ValidateUserName(userName))
            throw new EditorException("Array item user defined name is not in correct format");

        if (!ValidatePositions(positions))
            throw new EditorException("Array item positions are not in correct format");

        if (!ValidateType(type))
            throw new EditorException("Array item type is not in correct format");

        this.userName = userName;
        this.positions = positions;
        this.type = type;

        wholeName = GenerateSerializedName();
    }

    public string GenerateSerializedName()
    {
        string generatedName = "#";

        foreach (int pos in positions)
            generatedName += pos.ToString() + ",";

        generatedName = generatedName.Substring(0, generatedName.Length - 1);

        generatedName += " [";
        generatedName += type.ToString();
        generatedName += "] ";

        generatedName += userName;

        return generatedName;
    }
}
