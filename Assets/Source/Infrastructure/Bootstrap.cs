using Source.Services;
using UnityEngine;

namespace Source.Infrastructure
{
    public class Bootstrap : MonoBehaviour
    {
        private static bool _isInitialized;

        [SerializeField] private SceneObjectsProvider _sceneObjectsProvider;

        private void Start()
        {
            if (!_isInitialized)
            {
                Initialize();
                return;
            }

            Services services = Services.Container;
            services.ForceRegister<ISceneObjectsProvider>(_sceneObjectsProvider);
            IScenesService scenesService = services.Single<IScenesService>();
            
            if (scenesService.IsGame())
            {
                services.Single<IQuestionPickService>().Start();
            }
        }

        private void Initialize()
        {
            Services services = Services.Container;

            IAssetsProvider assetsProvider = new AssetsProvider();
            IQuestionsService questionsService = new QuestionsService(assetsProvider);
            IScenesService scenesService = new ScenesService();
            IAnswersStatisticsService answersStatisticsService = new AnswersStatisticsService();
            IQuestionPickService questionPickService = new QuestionPickService(questionsService, 
                answersStatisticsService);

            services.RegisterSingle<IAssetsProvider>(assetsProvider);
            services.RegisterSingle<IQuestionsService>(questionsService);
            services.RegisterSingle<IScenesService>(scenesService);
            services.RegisterSingle<IQuestionPickService>(questionPickService);
            services.RegisterSingle<IAnswersStatisticsService>(answersStatisticsService);
            services.RegisterSingle<ISceneObjectsProvider>(_sceneObjectsProvider);
            
            _isInitialized = true;
            scenesService.LoadMenu();
        }
    }
}
