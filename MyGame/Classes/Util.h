//
//  Util.h
//  MyGame
//
//  Created by 孔 令锴 on 14-5-9.
//
//

#ifndef __MyGame__Util__
#define __MyGame__Util__

#include <iostream>

class Util
{
protected:
    Util();
    ~Util();
    
public:
    static Util* getInstance();
    
    std::string formatString(const std::string,...);
    
};

#endif /* defined(__MyGame__Util__) */
