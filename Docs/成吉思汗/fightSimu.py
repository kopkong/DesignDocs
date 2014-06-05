import random,time

class Fighter:
	'Store fighter properties'


F1 = Fighter()
F1.name= "player"
F1.a = 100
F1.d = 25
F1.hp = 200

def getDamage(A,B):
	point = max(A.a - B.d,A.a/2)
	A.hp -= point

def levelUp(U):
	U.a += 


if __name__ == '__main__':
	
	