module.exports = {
  // ...other vue-cli plugin options...
  pwa: {
    name: 'Vue PWA Rapid Prototype',
    short_name: 'Vue IDD PWA',
    theme_color: 'green',
    background_color: 'white',
    display: 'standalone',
    start_url: '.',
    iconPaths: {
      favicon16: 'img/icons/favicon-32x32.ico',
      favicon32: 'img/icons/favicon-32x32.ico',
      icons: [
        {
            src: 'assets/icons/clown_pineapple-192x192.png',
            sizes: '192x192'
        },
        {
            src: 'assets/icons/clown_pineapple-512x512.png',
            sizes: '512x512'
        }
      ]
    },
    // configure the workbox plugin
    // Must change this for custom service worker file
    workboxPluginMode: 'GenerateSW',
  }
}
