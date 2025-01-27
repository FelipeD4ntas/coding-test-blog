<template>
  <v-container>
    <v-card variant="text" class="ma-10 custom-ma d-flex justify-center align-center" color="red" flat>
      <h1>BLOG Simples</h1>
    </v-card>
    
    <v-form>
      <v-label class="custom-label-color" text="Nome"></v-label>
      <v-text-field 
        v-model.trim="user.nome" 
        variant="outlined" 
        name="nome" 
        placeholder="Seu nome"
        type="nome">
      </v-text-field>

      <v-label class="custom-label-color" text="E-mail"></v-label>
      <v-text-field 
        v-model.trim="user.email" 
        variant="outlined" 
        name="email" 
        placeholder="Seu e-mail"
        type="email">
      </v-text-field>

      <v-label class="custom-label-color" text="Senha"></v-label>
      <v-text-field 
        v-model.trim="user.senha" 
        variant="outlined" 
        name="senha" 
        placeholder="Sua senha"
        type="senha">
      </v-text-field>

      <v-label class="custom-label-color" text="Confirmação Senha"></v-label>
      <v-text-field 
        v-model.trim="user.confirmacaoSenha" 
        variant="outlined" 
        name="confirmacaoSenha" 
        placeholder="Corfirme sua senha"
        type="confirmacaoSenha">
      </v-text-field>

      <v-container class="d-flex justify-center">
        <v-card-text>
          <v-btn block variant="text" class="custom-bg pa-10 custom-pa font-weight-regular text-uppercase text-body-1"  @click="voltar">Voltar</v-btn>
        </v-card-text>
        <v-card-text>
          <v-btn block variant="text" class="bg-primary pa-10 custom-pa font-weight-regular text-uppercase text-body-1" @click="submit">Cadastrar</v-btn>
        </v-card-text>
      </v-container>
    </v-form>
  </v-container>
</template>

<script>
import { servicesUser } from '@/modules/cadastro/services'

export default {
  name: 'CadastroView',
  data() {
    return {
      user: {
        nome: '',
        email: '',
        senha: '',
        confirmacaoSenha: ''
      }
    }
  },
  methods: {
    voltar() {
      this.$router.push(this.$route.query.redirect || '/login');
    },
    async submit() {
      this.isLoading = true;

      const bodyUser =  {
          nome: this.user.nome,
          email: this.user.email,
          senha: this.user.senha,
          confirmacaoSenha: this.user.confirmacaoSenha
      }

      try {
        const response = await servicesUser.adicionar('usuario', bodyUser);
        this.$router.push(this.$route.query.redirect || '/login');
        console.log(response);
        window.alert(response.data.dados.mensagem);
        this.isLoading = false;
      } catch(e) {
        this.isLoading = false;

        window.alert('Erro ao cadastrar usuário');

        console.log(e);
      }
    }
  }
}
</script>

<style scoped>
.custom-bg {
  background-color: gray;
  color: white;
}

.titulo-login {
  white-space: wrap;
}

span {
  color: #F30168;
  text-decoration: underline;
  text-decoration-thickness: 1px;
  cursor: default;
}

.custom-subtitle {
  color: black !important;
  font-weight: 600;
}
</style>