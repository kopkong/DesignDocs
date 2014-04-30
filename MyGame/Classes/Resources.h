//
//  Resources.h
//  MyGame
//
//  Created by 孔 令锴 on 14-4-28.
//
//

#ifndef __MyGame__Resources__
#define __MyGame__Resources__

#include <iostream>

class Resources
{
private:
    std::string _2DCharacterDirectory;
    std::string _menuItemDirectory;
    
protected:
	Resources(void);
	~Resources();

public:
    static Resources* getInstance();
    
    std::string getFootmanResourceA();
    std::string getFootmanResourceB();
    
    std::string getKnightResourceA();
    std::string getKnightResourceB();
    
    std::string getArcherResourceA();
    std::string getArcherResourceB();
    
    std::string getFormationBoard();
    std::string getStartBattleButton();
    std::string getMenuItem0();
    std::string getMenuItem1();
    std::string getMenuItem2();
    std::string getMenuItem3();
};

#endif /* defined(__MyGame__Resources__) */
