//
//  GameCore.h
//  Game2
//
//  Created by 孔 令锴 on 14-5-20.
//
//

#ifndef __Game2__GameCore__
#define __Game2__GameCore__

#include <iostream>
#include <memory>
#include "Monster.h"
#include "Player.h"

using namespace std;

class CoreGame
{
private:
    unsigned int _gameRound;
    Player* _pPlayer;
    Monster* _pMonster;
    unsigned int _playerSelectedNumber;
    unsigned int _monsterSelectedNumber;
    bool _isEnd;
    
    
protected:
    CoreGame();
    ~CoreGame();
    friend class auto_ptr<CoreGame>;
    static auto_ptr<CoreGame> _instance;
    
public:
    static CoreGame* getInstance();
    unsigned int getCurrentRound();
    void roundEnd();
    void reset();
    void addPlayer(Player*);
    void addMonster(Monster*);
    void playerSelectNumber(unsigned int);
    void monsterSelectNumber(unsigned int);
    void end();
};

#endif /* defined(__Game2__GameCore__) */
