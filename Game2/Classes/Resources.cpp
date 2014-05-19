//
//  Resources.cpp
//  Game2
//
//  Created by 孔 令锴 on 14-5-17.
//
//

#include "Resources.h"
#include <string>

Resources::Resources()
{
#ifdef WIN32
    _menuItemDirectory = "../Resources/";
#else
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

std::string Resources::getMenuLayerBackGround()
{
    return _menuItemDirectory + "bg1.jpg";
}

std::string Resources::getMenuButton1()
{
    return _menuItemDirectory + "btn-choose-level.png";
}

std::string Resources::getMenuButton2()
{
    return _menuItemDirectory +"btn-history.png";
}

std::string Resources::getMenuButton3()
{
    return _menuItemDirectory + "btn-help.png";
}

std::string Resources::getLevelButton1()
{
	return _menuItemDirectory + "menuLevel1.png";
}

std::string Resources::getLevelButton2()
{
	return _menuItemDirectory + "menuLevel2.png";
}

std::string Resources::getLevelButton3()
{
	return _menuItemDirectory + "menuLevel3.png";
}

std::string Resources::getGameLayerBackGround()
{
    return _menuItemDirectory + "bg.png";
}

std::string Resources::getGrid()
{
    return _menuItemDirectory + "grid.jpeg";
}
