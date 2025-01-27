<template>
  <v-row justify="center">
    <v-col xs="12" sm="10" md="10" lg="10" xl="10">
      <v-card class="custom-v-card-dashboad" elevation="0">
        <v-layout>
          <v-app-bar color="background" elevation="0">
            <template v-slot:prepend>
              <v-card
                variant="text"
                class="ma-10 custom-ma d-flex justify-center align-center"
                color="red"
                flat
              >
                <h1><span class="blue">Seja bem-vindo ao </span>BLOG Simples</h1>
              </v-card>
            </template>
            <v-spacer></v-spacer>
            <span class="custom-titulo mr-4">{{ userName }}</span>
            <v-btn icon @click="value = !value">
              <v-icon>mdi-account</v-icon>
            </v-btn>
            <v-btn v-show="value" @click="logout">
              Sair
            </v-btn>
            <v-btn v-show="!userId" @click="irParaLogin">
              Fazer Login
            </v-btn>
            <v-btn v-show="!userId" @click="irParaCadastro">
              Cadastre-se
            </v-btn>
          </v-app-bar>

          <v-main class="custom-v-main">
            <v-container fluid>
              <v-card class="pa-4 mb-4" v-show="userId">
                <v-text-field
                  label="Título do Post"
                  v-model="postForm.titulo"
                  outlined
                  dense
                ></v-text-field>
                <v-textarea
                  label="Conteúdo"
                  v-model="postForm.conteudo"
                  outlined
                  dense
                ></v-textarea>
                <v-btn
                  color="primary"
                  class="mt-2"
                  @click="savePost"
                  :disabled="loading"
                >
                  {{ postForm.id ? 'Editar Post' : 'Adicionar Post' }}
                </v-btn>
              </v-card>

              <v-card
                v-for="post in posts"
                :key="post.id"
                class="mb-3"
                outlined
              >
                <v-card-title>{{ post.titulo }}</v-card-title>
                <v-card-subtitle>
                  Por {{ post.nomeAutor }} em {{ formatarData(post.dataPublicacao) }}
                </v-card-subtitle>
                <v-card-text>{{ post.conteudo }}</v-card-text>
                <v-card-actions>
                  <v-btn v-show="userId && post.autorId === userId" color="primary" @click="editPost(post)">Editar</v-btn>
                  <v-btn v-show="userId && post.autorId === userId" color="red" @click="deletePost(post.id)">Excluir</v-btn>
                </v-card-actions>
              </v-card>
            </v-container>
          </v-main>
        </v-layout>
      </v-card>
    </v-col>
  </v-row>

  <v-snackbar v-model="newPostNotification" timeout="5000">
    Nova postagem foi adicionada!
  </v-snackbar>
</template>

<script>
import { servicesPost } from '@/modules/dashboard/services'

export default {
  name: 'DashboardView',
  data() {
    return {
      userName: '',
      userId: '',
      value: false,
      posts: [],
      postForm: {
        id: null,
        titulo: '',
        conteudo: ''
      },
      loading: false,
      newPostNotification: false,
      webSocket: null 
    };
  },
  created() {
    this.userName = localStorage.getItem('USER_NAME') || 'Usuário';
    this.userId = localStorage.getItem('USER_ID') || null;
    this.fetchPosts();
    this.initializeWebSocket(); 
  },
  unmounted() {
    if (this.webSocket) {
      this.webSocket.close();
    }
  },
  methods: {
    irParaLogin() {
      this.$router.push('/login');
    },
    irParaCadastro() {
      this.$router.push('/cadastro');
    },
    initializeWebSocket() {
      const protocol = window.location.protocol === "https:" ? "wss" : "ws";
      const wsUrl = `${protocol}://localhost:7164/ws`;
      this.webSocket = new WebSocket(wsUrl);

      this.webSocket.onopen = () => {
        console.log("Conexão WebSocket aberta");
      };

      this.webSocket.onmessage = (event) => {
        console.log("Mensagem recebida:", event.data);
        if (event.data === "Novo post publicado!") {
          this.newPostNotification = true;
          this.fetchPosts(); 
        }
      };

      this.webSocket.onerror = (error) => {
        console.error("Erro no WebSocket:", error);
      };

      this.webSocket.onclose = () => {
        console.warn("Conexão WebSocket fechada");
      };
    },
    async fetchPosts() {
      try {
        const response = await servicesPost.listar('postagem/postagens');
        this.posts = response.data.dados.postagens;
      } catch (error) {
        console.error('Erro ao buscar posts:', error);
      }
    },
    async savePost() {
      try {
        this.loading = true;
        if (this.postForm.id) {
          await servicesPost.editar(`postagem/${this.postForm.id}`, this.postForm);
        } else {
          await servicesPost.adicionar('postagem', this.postForm);
          this.snackbar = true; 
        }
        this.postForm = { id: null, titulo: '', conteudo: '' };
        this.fetchPosts();
      } catch (error) {
        console.error('Erro ao salvar post:', error);
      } finally {
        this.loading = false;
      }
    },
    editPost(post) {
      this.postForm = { ...post };
    },
    async deletePost(postId) {
      try {
        await servicesPost.deletar(`postagem/${postId}`);
        this.fetchPosts();
      } catch (error) {
        console.error('Erro ao excluir post:', error);
      }
    },
    logout() {
      localStorage.removeItem('AUTH_TOKEN');
      localStorage.removeItem('USER_NAME');
      localStorage.removeItem('USER_ID');
      this.$router.push(this.$route.query.redirect || '/dashboard');
    },
    formatarData(data) {
      const options = { day: '2-digit', month: '2-digit', year: 'numeric' };
      return new Date(data).toLocaleDateString('pt-BR', options);
    }
  }
};
</script>

<style>
.custom-row-login {
  overflow: auto;
}

.titulo-login {
  font-size: 29px;
  font-weight: 900;
}

.card-padding {
  padding: 50px;
}

.sub-titulo-login {
  font-size: 16px;
  font-weight: 500;
  white-space: wrap;
  color: black !important;
}

.custom-label-color {
  color: black !important;
  font-weight: 500;
}

.v-btn--no-padding {
  padding: 0 !important;
}

.v-btn--text-no-uppercase .v-btn__content {
  text-transform: none !important;
}

.custom-text-field .v-field__input {
  height: 70px !important; /* Ajuste o valor da altura conforme necessário */
}

.v-responsive.v-img.custom-v-img {
  margin: 0 auto;
}

.box-link-criar-conta {
  display: flex;
  justify-conteudo: center;
  margin-top: 36px;
}

.box-link-criar-conta p {
  font-weight: 400;
  font-size: 20px;
}

.box-link-criar-conta span {
  color: #F30168;
  text-decoration: underline;
  text-decoration-thickness: 1px;
  cursor: pointer;
}

@media(max-width: 590px) {
  .titulo-login {
    font-size: 26px;
  }
  
  .sub-titulo-login {
    font-size: 13px;
  }

  .custom-ma {
    margin: 20px !important;
  }

  .custom-pa {
    padding: 25px !important;
  }

  .box-link-criar-conta {
    margin-top: 20px;
  }

  .custom-v-card-text.v-card-text {
    padding-top: 0px !important;
  }
}
</style>