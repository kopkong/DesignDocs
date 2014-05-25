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
    
}

CoreGame::~CoreGame()
{
    delete _pPlayer;
    delete _pMonster;
}

CoreGame* CoreGame::getInstance()
{
    if(0 == _instance.get())
    {
        _instance.reset(new CoreGame);
    }
    return _instance.get();
}

void CoreGame::reset()
{
    _gameRound = 0;
    _playerSelectedNumber = 0;
    _monsterSelectedNumber = 0;
    _pPlayer = NULL;
    _pMonster = NULL;
    _isEnd = false;
}

void CoreGame::addMonster(Monster* m)
{
    _pMonster = m;
}

void CoreGame::addPlayer(Player* p)
{
    _pPlayer = p;
}

void CoreGame::playerSelectNumber(unsigned int n)
{
    _playerSelectedNumber = n;
}

void CoreGame::monsterSelectNumber(unsigned int n)
{
    _monsterSelectedNumber = n;
}

void CoreGame::roundEnd()
{
    if(_isEnd)
    {
        _gameRound = 0;
    }
    else
    {
        _gameRound ++;
        
        // compute the damage
        if(_playerSelectedNumber > _monsterSelectedNumber)
            _pMonster->beAttacked(_playerSelectedNumber - _monsterSelectedNumber);
        else if(_monsterSelectedNumber > _playerSelectedNumber)
            _pPlayer->beAttacked(_monsterSelectedNumber - _playerSelectedNumber);
        
        // clear the number
        _playerSelectedNumber = 0;
        _monsterSelectedNumber = 0;
    }
}

unsigned int CoreGame::getCurrentRound()
{
    return _gameRound;
}

void CoreGame::end()
{
    _isEnd = true;
}

