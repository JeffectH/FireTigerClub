using System;

public interface IInput
{
    event Action RightMove;
    event Action LeftMove;
}