//
//  Resources.cpp
//  MyGame
//
//  Created by 孔 令锴 on 14-4-28.
//
//

#include "Resources.h"
#include <string>
#include "Util.h"

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

CharacterRes Resources::getFootmanResourceA()
{
    CharacterRes r;
	
#ifdef WIN32
    std::string dir = "Character/FootmanA/";
#else
    std::string dir = "";
#endif
    
    std::vector<std::string> idles;
    std::vector<std::string> moves;
    
    for(int i = 1; i <=4 ; i++)
    {
        idles.push_back(dir + Util::getInstance()->formatString("1-%d.png",i));
    }
    
    for(int i = 1; i <= 8 ;i ++)
    {
        moves.push_back(dir + Util::getInstance()->formatString("2-%d.png",i));
    }
    
    r.Idle = idles;
    r.Move = moves;
    
    return r;
}

CharacterRes Resources::getKnightResourceA()
{
    CharacterRes r;
	
#ifdef WIN32
    std::string dir = "Character/KnightA/";
#else
    std::string dir = "";
#endif
    
    std::vector<std::string> idles;
    std::vector<std::string> moves;
    
    for(int i = 1; i <=4 ; i++)
    {
        idles.push_back(dir + Util::getInstance()->formatString("5-%d.png",i));
    }
    
    for(int i = 1; i <= 8 ;i ++)
    {
        moves.push_back(dir + Util::getInstance()->formatString("6-%d.png",i));
    }
    
    r.Idle = idles;
    r.Move = moves;
    
    return r;
    

}

CharacterRes Resources::getArcherResourceA()
{
    CharacterRes r;
	
#ifdef WIN32
    std::string dir = "Character/ArcherA/";
#else
    std::string dir = "";
#endif
    
    std::vector<std::string> idles;
    std::vector<std::string> moves;
    
    for(int i = 1; i <=4 ; i++)
    {
        idles.push_back(dir + Util::getInstance()->formatString("9-%d.png",i));
    }
    
    for(int i = 1; i <= 8 ;i ++)
    {
        moves.push_back(dir + Util::getInstance()->formatString("10-%d.png",i));
    }
    
    r.Idle = idles;
    r.Move = moves;
    
    return r;
}

CharacterRes Resources::getFootmanResourceB()
{
    CharacterRes r;
	
#ifdef WIN32
    std::string dir = "Character/FootmanB/";
#else
    std::string dir = "";
#endif
    
    std::vector<std::string> idles;
    std::vector<std::string> moves;
    
    for(int i = 1; i <=4 ; i++)
    {
        idles.push_back(dir + Util::getInstance()->formatString("3-%d.png",i));
    }
    
    for(int i = 1; i <= 8 ;i ++)
    {
        moves.push_back(dir + Util::getInstance()->formatString("4-%d.png",i));
    }
    
    r.Idle = idles;
    r.Move = moves;
    
    return r;

}

CharacterRes Resources::getKnightResourceB()
{
    CharacterRes r;
	
#ifdef WIN32
    std::string dir = "Character/KnightB/";
#else
    std::string dir = "";
#endif
    
    std::vector<std::string> idles;
    std::vector<std::string> moves;
    
    for(int i = 1; i <=4 ; i++)
    {
        idles.push_back(dir + Util::getInstance()->formatString("7-%d.png",i));
    }
    
    for(int i = 1; i <= 8 ;i ++)
    {
        moves.push_back(dir + Util::getInstance()->formatString("8-%d.png",i));
    }
    
    r.Idle = idles;
    r.Move = moves;
    
    return r;
    

}

CharacterRes Resources::getArcherResourceB()
{
    CharacterRes r;
	
#ifdef WIN32
    std::string dir = "Character/ArcherB/";
#else
    std::string dir = "";
#endif
    
    std::vector<std::string> idles;
    std::vector<std::string> moves;
    
    for(int i = 1; i <=4 ; i++)
    {
        idles.push_back(dir + Util::getInstance()->formatString("11-%d.png",i));
    }
    
    for(int i = 1; i <= 8 ;i ++)
    {
        moves.push_back(dir + Util::getInstance()->formatString("12-%d.png",i));
    }
    
    r.Idle = idles;
    r.Move = moves;
    
    return r;
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

std::string Resources::getStartBattleButton2()
{
    return _menuItemDirectory + "start10.png";
}

std::string Resources::getRandomNPCFormationButton()
{
	return _menuItemDirectory + "randomNPC.png";
}

std::string Resources::getRandomPlayerFormationButton()
{
	return _menuItemDirectory + "randomPlayer.png";
}

std::string Resources::getPlayIcon()
{
    return _menuItemDirectory + "play1normal.png";
}

std::string Resources::getPauseIcon()
{
    return _menuItemDirectory + "pausenormal.png";
}

std::string Resources::getStringSquadType(SquadType type)
{
	switch (type)
	{
	case None:
		return "空白";
	case Footman:
		return "步兵";
	case Knight:
		return "骑兵";
	case Archer:
		return "弓兵";
	default:
		return "空白";
	}

}