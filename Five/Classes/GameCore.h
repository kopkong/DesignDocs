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

using namespace std;

enum CoreGameState
{
    NotStarted,
    Running,
    Finishing,
    End
};

class CoreGame
{
private:
    unsigned int _gameRound;
    int _playerSelectedNumber;
    int _monsterSelectedNumber;
    bool _isEnd;
    CoreGameState _state;
    
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
    
    CoreGameState getState();
    
    void setPlayerWin();
    void setMonsterWin();
    
    void monsterAttack();
    void playerAttack();
    
    void playerSelectNumber( int);
    void monsterSelectNumber( int);
    void end();
};

#endif /* defined(__Game2__GameCore__) */
