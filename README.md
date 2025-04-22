# **VFX de Escuridão**  

## **Visão Geral**  
Este projeto consiste em um efeito de escuridão dinâmico feito usando URP na Unity 6. Eu optei por um shader e uma partícula por serem de mais fácil acesso para o time de arte por funcionarem direto no Inspector da Unity e poderem ter suas propriedades animadas também com facilidade, esse shader simula uma névoa que aumenta ou diminui de acordo com a potência de uma tocha no centro, cobrindo tudo fora de seu raio de iluminação com escuridão combinando com uma partícula de fumaça para trazer uma transição mais suave.
### **Características gerais** 
- Um shader baseado em distância e animação de noise texture aplicado a todos os objetos do cenário  
- Partículas de fumaça densa (VFX Graph) que aparecem nas bordas da área iluminada 

---

## **Decisões Artísticas e Técnicas**

### **Shader de Escuridão**

  - Objetos fora do raio da tocha têm aparência alterada para representar uma escurição profunda (cores escuras, redução de brilho e da suavidade da superficie).
  - Transição suave usando **Smoothstep** para evitar bordas duras.
  - Uso de noise texture para dar um aspecto de movimento natural na transição entre o escuro e o claro.
  - Uso de **Triplanar Mapping** para evitar distorções em superfícies verticais.
  - A posição da tocha (`_Position`) é ajustada em tempo real via script.
  - O raio da tocha (`lightRadius`) é ajustável e animável através do Inspector.

### **Sistema de Partículas (Fumaça)**  

  - As partículas são mais densas próximas às bordas da luz, simulando pressão contra a iluminação.
  - São direcionadas de dentro para fora, criando a ilusão de que a escuridão está "empurrando" contra a tocha.
  - O raio do spawn das partículas (`smokeRadius`) é ajustável e animável através do Inspector.
  - Todas as propriedades de tamanho, velocidade, transparencia, cor e e textura da particula são ajustáveis e animáveis no Inspector.

### **Detalhes** 

  - O noise do shader por conta de sua animação traz um efeito de sombra ao se posicionar logo abaixo das partículas.
  - O raio da fumaça (`smokeRadius`) é sincronizado com o raio da luz (`lightRadius`), mas uma unidade menor para evitar sobreposição brusca.
  - A textura de noise foi criada no **Material Maker**.

---

## **Como Funciona**  
1. **Shader:**  
   - Calcula a distância entre cada pixel na cena e posição da tocha (Distance node).
   - Divide pelo (`lightRadius`) para normalizar (transforma em um valor entre 0 e 1).
   - Esse resultado invertido cria uma máscara redonda ao redor da posição da tocha. 
   - Multiplica essa máscara de escuridão por um noise em movimento para gerar um efeito de fumaça na transição da luz e sombra.   
   - Define cores e transiciona propriedades de material (smoothness e ambient oclusion) com base na máscara.
   - O shader foi feito para ser usado em todos os materiais do jogo que irão interagir com a luz da tocha, então as outras propriedades básicas (smoothness, transparency, metalness, normal e ambient oclusion) de materiais estão presentes.

2. **Partículas (VFX Graph):**  
   - Tamanho do spawn controlado pela propriedade (`smokeRadius`) no script.
   - Quantidade de partículas ajustável para garantir a otimização em jogo.
   - Utilização de normal map nas partículas para um visual mais atmosferico.

3. **Controle via Script (`ShaderPosition`):**  
   - Gerencia o raio da luz (`lightRadius`) e da fumaça (`smokeRadius`).
   - Permite sincronização automática ou ajuste manual dos efeitos.
   - Adiciona visualização dos raios da luz e fumaça no Editor da Unity

---

## **Demonstração**  
A cena de padrão (`SampleScene.Unity`) inclui:  
- Objetos e texturas básicos para demonstrar os efeitos.  
- Um objeto nomeado tocha que age como o pivo do efeito.  
 -Uma animação simples mostrando a escuridão avançando conforme o raio diminui e o efeito acompanhando o movimento da tocha.  

---

Esse efeito foi projetado para ser flexível e otimizado, pronto para implementação em um pipeline de produção.

--- 

**Observação**: a textura da partícula de fumaça (WispySmoke02) é parte de um pacote gratuíto de domínio público disponibilizado pela Unity: https://unity.com/pt/blog/engine-platform/free-vfx-image-sequences-flipbooks.
