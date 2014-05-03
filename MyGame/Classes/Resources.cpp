//
//  Resources.cpp
//  MyGame
//
//  Created by 孔 令锴 on 14-4-28.
//
//

#include "Resources.h"
#include <string>

Resources::Resources()
{
#ifdef WIN32
    _2DCharacterDirectory = "2D-Cartoon Vector Characters/";
    _menuItemDirectory = "Menu/";
#else
    _2DCharacterDirectory = "";
    _menuItemDirectory = "";
#endif
    
}

Resources::~Resources()
{
    
}

Resources* Resources::getInstance()
{
    static Resources s_Resources;
    return &s_Resources;
}

std::string Resources::getFootmanResourceA()
{
	return _2DCharacterDirectory + "dwarf_warrior.png";
}

std::string Resources::getKnightResourceA()
{
    return _2DCharacterDirectory + "viking.png";
}

std::string Resources::getArcherResourceA()
{
    return _2DCharacterDirectory + "archer.png";
}

std::string Resources::getFootmanResourceB()
{
    return _2DCharacterDirectory + "orc.png";
}

std::string Resources::getKnightResourceB()
{
    return _2DCharacterDirectory + "centaur.png";
}

std::string Resources::getArcherResourceB()
{
    return _2DCharacterDirectory + "dragon.png";
}

std::string Resources::getMenuItem0()
{
    return _menuItemDirectory + "item0.png";
}

std::string Resources::getMenuItem1()
{
    return _menuItemDirectory + "item1.png";
}

std::string Resources::getMenuItem2()
{
    return _menuItemDirectory + "item2.png";
}

std::string Resources::getMenuItem3()
{
    return _menuItemDirectory + "item3.png";
}

std::string Resources::getFormationBoard()
{
    return _menuItemDirectory + "board.png";
}

std::string Resources::getStartBattleButton()
{
    return _menuItemDirectory + "start.png";
}
