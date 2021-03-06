﻿//
//  Resources.h
//  MyGame
//
//  Created by 孔 令锴 on 14-4-28.
//
//

#ifndef __MyGame__Resources__
#define __MyGame__Resources__

#include <iostream>
#include "Squad.h"


class Resources
{
private:
    std::string _2DCharacterDirectory;
    std::string _menuItemDirectory;
    std::string _characterDirectory;
    
protected:
	Resources(void);
	~Resources();

public:
    static Resources* getInstance();
    
    CharacterRes getFootmanResourceA();
    CharacterRes getFootmanResourceB();
    
    CharacterRes getKnightResourceA();
    CharacterRes getKnightResourceB();
    
    CharacterRes getArcherResourceA();
    CharacterRes getArcherResourceB();
    
    std::string getFormationBoard();
    std::string getStartBattleButton();
    std::string getStartBattleButton2();
	std::string getRandomPlayerFormationButton();
	std::string getRandomNPCFormationButton();
    std::string getMenuItem0();
    std::string getMenuItem1();
    std::string getMenuItem2();
    std::string getMenuItem3();
    std::string getPlayIcon();
    std::string getPauseIcon();

	std::string getStringSquadType(SquadType);
	std::string getBackground();
	std::string getFontName();
};

#endif /* defined(__MyGame__Resources__) */
