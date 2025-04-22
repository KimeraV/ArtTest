# **VFX de Escuridão**  

## **Visão Geral**  
Este projeto consiste em um efeito de escuridão dinâmico criado usando URP na Unity 6. Optei por utilizar um shader combinado com partículas por serem soluções de fácil acesso para o time de arte, funcionando diretamente no Inspector da Unity e permitindo animação simplificada das propriedades. O shader simula uma névoa que se expande ou contrai de acordo com a intensidade de uma tocha central, cobrindo tudo fora de seu raio de iluminação com escuridão, enquanto partículas de fumaça garantem uma transição suave.

### **Características Gerais** 
- Shader baseado em distância com animação de noise texture aplicado a todos os objetos do cenário  
- Partículas de fumaça densa (VFX Graph) posicionadas nas bordas da área iluminada  

---

## **Decisões Artísticas e Técnicas**

### **Shader de Escuridão**

  - Objetos fora do raio da tocha têm sua aparência alterada para representar escuridão profunda (cores escuras, redução de brilho e suavidade da superfície)
  - Transição suave utilizando **Smoothstep** para evitar bordas duras
  - Noise texture animada para criar movimento orgânico na transição entre luz e sombra
  - **Triplanar Mapping** para prevenir distorções em superfícies verticais
  - Posição da tocha (`_Position`) ajustada em tempo real via script
  - Raio da tocha (`lightRadius`) configurável e animável diretamente no Inspector

### **Sistema de Partículas (Fumaça)**  

  - Maior densidade de partículas próximo às bordas da luz, simulando pressão contra a iluminação
  - Direcionamento de dentro para fora, criando a ilusão de escuridão "empurrando" contra a tocha
  - Raio de spawn das partículas (`smokeRadius`) ajustável e animável via Inspector
  - Todas as propriedades (tamanho, velocidade, transparência, cor e textura) configuráveis e animáveis no Inspector

### **Detalhes** 

  - O noise animado do shader cria efeito de sombra quando posicionado abaixo das partículas
  - O raio da fumaça (`smokeRadius`) é sincronizado com o raio da luz (`lightRadius`), porém uma unidade menor para evitar sobreposição brusca
  - A textura de noise foi criada no **Material Maker**

---

## **Funcionamento**  
1. **Shader:**  
   - Calcula a distância entre cada pixel da cena e a posição da tocha (nó Distance)
   - Divide pelo `lightRadius` para normalizar (valores entre 0 e 1)
   - Inverte o resultado para criar uma máscara circular ao redor da tocha
   - Multiplica a máscara por um noise animado para gerar efeito de fumaça na transição
   - Define cores e transiciona propriedades materiais (smoothness e ambient occlusion) com base na máscara
   - Desenvolvido para aplicação universal em todos os materiais que interagem com a luz da tocha, mantendo propriedades básicas (smoothness, transparência, metalness, normal e ambient occlusion)

2. **Partículas (VFX Graph):**  
   - Área de spawn controlada pela propriedade `smokeRadius` no script
   - Quantidade de partículas ajustável para otimização de performance
   - Utiliza normal map para maior riqueza visual

3. **Controle via Script (`ShaderPosition`):**  
   - Gerencia os raios de luz (`lightRadius`) e fumaça (`smokeRadius`)
   - Permite sincronização automática ou ajuste manual dos efeitos
   - Inclui visualização dos raios no Editor da Unity

---

## **Demonstração**  
A cena padrão (`SampleScene.unity`) inclui:  
- Objetos e texturas básicos para demonstração dos efeitos  
- Um objeto nomeado "Tocha" que atua como centro do efeito  
- Animação simples mostrando a escuridão avançando conforme o raio diminui e acompanhando o movimento da tocha  

---

Este efeito foi projetado para ser flexível e otimizado, pronto para implementação em pipeline de produção.

--- 

**Observação**: A textura da partícula de fumaça (WispySmoke02) é parte de um pacote gratuito de domínio público disponibilizado pela Unity: https://unity.com/pt/blog/engine-platform/free-vfx-image-sequences-flipbooks. O normal map utilizado foi gerado por mim no programa Materialize.
