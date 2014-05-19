//
//  Resources.h
//  Game2
//
//  Created by 孔 令锴 on 14-5-17.
//
//

#ifndef __Game2__Resources__
#define __Game2__Resources__

#include <iostream>

class Resources
{
private:
    std::string _menuItemDirectory;
    
protected:
	Resources(void);
	~Resources();
    
public:
    static Resources* getInstance();
    
    std::string getMenuLayerBackGround();
    std::string getMenuButton1();
    std::string getMenuButton2();
    std::string getMenuButton3();
    
	std::string getLevelButton1();
	std::string getLevelButton2();
	std::string getLevelButton3();
    
    std::string getGameLayerBackGround();
    std::string getGrid();
    
};


#endif /* defined(__Game2__Resources__) */
