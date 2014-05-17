//
//  Resources.cpp
//  Game2
//
//  Created by 孔 令锴 on 14-5-17.
//
//

#include "Resources.h"

Resources::Resources()
{
#ifdef WIN32
    _menuItemDirectory = "Menu/";
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
    return "menuBg.jpg";
}

std::string Resources::getMenuButton1()
{
    return "menuButton1.png";
}

std::string Resources::getMenuButton2()
{
    return "menuButton2.png";
}

std::string Resources::getMenuButton3()
{
    return "menuButton3.png";
}
