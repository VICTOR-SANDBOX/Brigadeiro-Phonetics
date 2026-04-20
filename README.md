# BRIGADEIRO/FLAVA (F-SAMPA)

**Fonética Simplificada para Síntese de Voz em Português Brasileiro utilizando o CVVX**

<img width="921" height="535" alt="image" src="https://github.com/user-attachments/assets/5ffcd49a-c54b-4012-8535-abf0a206652a" />

*VIICTOR CVVX Alpha v0, arte do voicebank feita por morip*

---

## Tabela Completa de Fonemas

### Vogais Orais

| BRIGADEIRO | IPA | Descrição | Exemplo |
|---------|-----|-----------|---------|
| `a` | /a/ | Vogal aberta central | c**a**sa, p**a**to |
| `e` | /e/ | Vogal semifechada anterior | d**e**do, m**e**sa |
| `i` | /i/ | Vogal fechada anterior | v**i**da, f**i**ta |
| `o` | /o/ | Vogal semifechada posterior | b**o**lo, m**o**do |
| `u` | /u/ | Vogal fechada posterior | l**u**a, m**u**do |

### Semivogais / Reduções

| BRIGADEIRO | IPA | Descrição | Exemplo |
|---------|-----|-----------|---------|
| `y` | /j/ | Semivogal anterior (redução de "i") | pa**i**, b**ei**jo |
| `w` | /w/ | Semivogal posterior (redução de "u") | ma**u**, s**ou** |

### Vogais Nasais

| BRIGADEIRO | IPA | Oral | Descrição | Exemplo |
|---------|-----|------|-----------|---------|
| `@` | /ã/ | `a` | Nasal de "a" | l**ã**, m**ã**e, c**an**ta |
| `7` | /ẽ/ | `e` | Nasal de "e" | t**en**to, v**en**to |
| `1` | /ĩ/ | `i` | Nasal de "i" | s**in**to, t**in**ta |
| `0` | /õ/ | `o` | Nasal de "o" | s**om**, b**om** |
| `Q` | /ũ/ | `u` | Nasal de "u" | m**un**do, f**un**do |

### Vogais Acentuadas

| BRIGADEIRO | Descrição |
|---------|-----------|
| `X` | "É" |
| `V` | "Ó" |

### Consoantes

#### Oclusivas

| BRIGADEIRO | IPA | Descrição | Exemplo |
|---------|-----|-----------|---------|
| `p` | /p/ | Oclusiva bilabial surda | **p**ato, ca**p**a |
| `b` | /b/ | Oclusiva bilabial sonora | **b**ola, a**b**rir |
| `t` | /t/ | Oclusiva alveolar/pós-alveolar surda | **t**ato, **t**ia |
| `d` | /d/ | Oclusiva alveolar/pós-alveolar sonora | **d**ado, **d**ia |
| `k` | /k/ | Oclusiva velar surda | **c**asa, **qu**ero |
| `g` | /ɡ/ | Oclusiva velar sonora | **g**ato, a**g**ora |

#### Fricativas

| BRIGADEIRO | IPA | Descrição | Exemplo |
|---------|-----|-----------|---------|
| `f` | /f/ | Fricativa labiodental surda | **f**aca, ca**f**é |
| `v` | /v/ | Fricativa labiodental sonora | **v**ida, a**v**ião |
| `s` | /s/ | Fricativa alveolar surda | **s**ala, pa**ss**o |
| `z` | /z/ | Fricativa alveolar sonora | ca**s**a, **z**ebra |
| `x` | /ʃ/ | Fricativa pós-alveolar surda | **ch**ave, a**ch**ar |
| `j` | /ʒ/ | Fricativa pós-alveolar sonora | **j**ogo, **g**elo |

#### Nasais

| BRIGADEIRO | IPA | Descrição | Exemplo |
|---------|-----|-----------|---------|
| `m` | /m/ | Nasal bilabial | **m**ão, ca**m**a |
| `n` | /n/ | Nasal alveolar | **n**ome, ca**n**a |
| `nh` | /ɲ/ | Nasal palatal | ba**nh**o, vi**nh**o |

#### Laterais

| BRIGADEIRO | IPA | Descrição | Exemplo |
|---------|-----|-----------|---------|
| `l` | /l/ | Lateral alveolar | **l**ua, ma**l**a |
| `lh` | /ʎ/ | Lateral palatal | fi**lh**o, o**lh**o |

#### Vibrantes e Tepes

| BRIGADEIRO | IPA | Descrição | Exemplo |
|---------|-----|-----------|---------|
| `r` | /ɾ/ | Tepe alveolar | ca**r**o, pa**r**a |
| `rr` | /h/ | Fricativa glotal/uvular | ca**rr**o, **r**ato |

---

## Phonemizer e Uso

> [!WARNING]
> **Aviso Importante:** Até o momento "CVVXPT-BRdoXiao.dll" é um phonemizer estritamente **fonético**, o que significa que ele não atua como G2P (Grapheme-to-Phoneme). Ou seja, você **não deve** escrever palavras completas na notação das notas. As notas devem ser preenchidas diretamente com os fonemas (em formato F-SAMPA) correspondentes a cada sílaba.

Este repositório contém o código fonte e as regras do **Phonemizer** para OpenUtau, otimizado para o padrão sintético de Português Brasileiro (CVVX e VCCV).
O phonemizer é responsável por interpretar a notação fonética inserida e transformá-la nos aliases corretos de acordo com as sílabas e as gravações disponíveis no banco de voz. Ele lida automaticamente com transições de consoante-vogal (CV), vogal-consoante (VC), e encontros vocálicos (VV), garantindo uma síntese de voz mais fluida e natural no formato CVVX.

**Como usar:**
Para utilizá-lo, mova o arquivo `.dll` para a pasta `Plugins` do OpenUtau. Ao configurar as propriedades da sua trilha de voz (track), basta selecionar o phonemizer correspondente em Português (PT) para que ele faça o mapeamento automático das suas entradas fonéticas.

### Dicas de Escrita e CCV

Para criar encontros consonantais do tipo **CCV** (como "pra", "tra", "vra"), recomenda-se decompor a transição utilizando o fonema de apoio **"e"**.

* **Exemplo:** `pra` → deve ser escrito como `pe` `ra`.
* **Ajuste:** A nota com o som `pe` deve ser mantida bem curta, servindo apenas para disparar o ataque da consoante `p`, transitando imediatamente para o `ra`.

> [!TIP]
> **Por que usar "e" em vez de schwa (vocal neutra)?**
> No Português Brasileiro, o uso do som **"e"** (fechado) para essas transições produz um resultado muito mais natural e fluido do que o uso de um schwa. Isso ocorre porque o "e" mantém o posicionamento do trato vocal mais próximo das gravações originais do banco, evitando o som "robótico" ou artificial que o schwa costuma introduzir nessas conexões.

## Recursos do Projeto e Banco de Voz

Além das regras fonéticas e do código fonte, a iniciativa provê recursos fundamentais para desenvolvedores de voz sintética:

* **Reclist:** Uma lista de gravação (*reclist*) detalhada, elaborada especificamente para dar suporte às regras e ao mapeamento esperado por este phonemizer (CVVX). Essencial para criação de novos voicebanks.
* **Configurações de oto.ini (oto.ini.ini):** O projeto fornece também um modelo base para o arquivo `oto.ini.ini`, englobando documentação e configurações de tempo fundamentais (como *Pre-utterance*, *Overlap* e *Consonant*), reduzindo a carga de trabalho na configuração inicial de um novo banco.
* **Voicebank "VIICTOR CVVX":** O projeto conta com um banco de voz para validação de conceitos e testes práticos, o **VIICTOR CVVX Alpha v0**. Como o nome sugere, ele está em estágio **Alpha**, atuando como um "protótipo" para comprovar os modelos de fonética. Mais à frente, esta demonstração será aprimorada e polida para se tornar uma síntese completa e aprimorada.

> [!IMPORTANT]
> **Avisos Críticos de oto.ini:**
>
> * **Som de "j":** Atualmente, o som de `j` deve ser cortado manualmente na oto.ini para formar o som de `dj`, pois esta configuração ainda não está presente na oto.ini base do projeto.
> * **Som de "x":** Atualmente, o som de `x` deve ser cortado manualmente na oto.ini para gerar o som de `tch`, pois esta configuração ainda não está presente na oto.ini base do projeto.

## Projeto

O BRIGADEIRO/FLAVA faz parte do ecossistema **BRIGADEIRO/FLAVA** para síntese de voz em Português Brasileiro.
