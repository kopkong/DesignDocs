//
//  Marco.h
//  RealFightSimu
//
//  Created by 孔 令锴 on 14-6-19.
//
//

#ifndef RealFightSimu_Marco_h
#define RealFightSimu_Marco_h

#include "cocos2d.h"

#define GetIntergerByKey(x) cocos2d::UserDefault::getInstance()->getIntegerForKey(x)
#define GetStringByKey(x) cocos2d::UserDefault::getInstance()->getStringForKey(x)
#define SetIntergerByKey(x,v) cocos2d::UserDefault::getInstance()->setIntegerForKey(x,v)
#define SetStringByKey(x,v) cocos2d::UserDefault::getInstance()->setStringForKey(x,v)



#endif
