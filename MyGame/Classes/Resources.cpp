//
//  Resources.cpp
//  MyGame
//
//  Created by 孔 令锴 on 14-4-28.
//
//

#include "Resources.h"

Resources::Resources()
{
#ifdef WIN32
    _osDirectory = "2D-Cartoon Vector Characters/";
#else
    _osDirectory = "";
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
    return _osDirectory + "dwarf_warrior.png";
}

std::string Resources::getKnightResourceA()
{
    return _osDirectory + "viking.png";
}

std::string Resources::getArcherResourceA()
{
    return _osDirectory + "archer.png";
}

std::string Resources::getFootmanResourceB()
{
    return _osDirectory + "orc.png";
}

std::string Resources::getKnightResourceB()
{
    return _osDirectory + "centaur.png";
}

std::string Resources::getArcherResourceB()
{
    return _osDirectory + "dragon.png";
}
