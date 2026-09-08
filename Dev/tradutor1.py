import json


# Esse arquivo é apenas uma gambiarra pra converter jsons que foram escritos de uma maneira
# diferente da que foi usada para o programa. Isso aqui é um arquivo que só tem utilidade para
# e durante o desenvolvimento.



arquivo = {}
with open("Files/Inst.json", "r") as arq:
    arquivo = json.load(arq)    

lista_das_notas = arquivo["notes"]
lista_corrigida = [ [nota["ms"], nota["lane"], 0] for nota in lista_das_notas  ]

print(lista_corrigida)

dicionario_final = {
    "nomeDaMusica": "ugh de fridaynightfunkin",
    "nomeDoArquivoDaMusica": "ugh.ogg",
    "notas": lista_corrigida
}

with open("Files/ugh.json", "w", encoding="utf-8") as arq:
    json.dump(dicionario_final, arq, indent=4, ensure_ascii=False)