//
//  GameCore.cpp
//  Game2
//
//  Created by 孔 令锴 on 14-5-20.
//
//

#include "GameCore.h"

auto_ptr<CoreGame> CoreGame::_instance;


CoreGame::CoreGame()
{
    _state = CoreGameState::NotStarted;
}

CoreGame::~CoreGame()
{
    log("~CoreGame");
    delete _player;
    delete _monster;
}

CoreGame* CoreGame::getInstance()
{
    if(0 == _instance.get())
    {
        log("Core game try to get new instance");
        _instance.reset(new CoreGame);
    }
    return _instance.get();
}

CoreGameState CoreGame::getState()
{
    return _state;
}

void CoreGame::reset()
{
    _gameRound = 0;
    _playerSelectedNumber = 0;
    _monsterSelectedNumber = 0;
    _state = CoreGameState::Running;
    //_isEnd = false;
}

void CoreGame::addMonster(Monster* m)
{
    _monster = m;
}

void CoreGame::addPlayer(Player *p)
{
    _player = p;
}

void CoreGame::monsterAttack()
{
    CCASSERT(_monsterSelectedNumber > 0, "Select number should between 1 and 9");
    _player->beAttacked(_monster, _monsterSelectedNumber);
    _monsterSelectedNumber = 0;
    
    if(_player->getDead())
        end();
}

void CoreGame::playerAttack()
{
    CCASSERT(_playerSelectedNumber > 0, "Select number should between 1 and 9");
    _monster->beAttacked(_player, _playerSelectedNumber);
    _playerSelectedNumber = 0;
    
    if(_player->getDead())
        end();
}

void CoreGame::playerSelectNumber( int n)
{
    _playerSelectedNumber = n;
}

void CoreGame::monsterSelectNumber( int n)
{
    _monsterSelectedNumber = n;
}

unsigned int CoreGame::getCurrentRound()
{
    return _gameRound;
}

void CoreGame::setPlayerWin()
{
    _state = CoreGameState::Finishing;
}

void CoreGame::setMonsterWin()
{
    _state = CoreGameState::Finishing;
}

void CoreGame::end()
{
    _state = CoreGameState::End;
    //_isEnd = true;
}

