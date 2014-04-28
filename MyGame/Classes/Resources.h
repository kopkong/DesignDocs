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
    std::string _osDirectory;
    
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
};

#endif /* defined(__MyGame__Resources__) */
