import '@mdi/font/css/materialdesignicons.css'
import { createVuetify } from 'vuetify'
import 'vuetify/styles'

const dotkonTheme = {
  colors: {
    background: '#FAFAFC',
    surface: '#FFFFFF',
    primary: '#F30168',
    secondary: '#03DAC6',
    error: '#B00020',
    info: '#2196F3',
    success: '#8D8D8D',
    warning: '#FB8C00'
  }
}
export default createVuetify(
  {
    theme: {
      defaultTheme: 'default',
      themes: {
        default: dotkonTheme
      },
    },
  }
)
